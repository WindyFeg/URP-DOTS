using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using System;
using Unity.Collections;
using Unity.Rendering;

public partial struct SpawnSystem : ISystem
{

    // [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {

        foreach (RefRW<Spawn> spawner in SystemAPI.Query<RefRW<Spawn>>())
        {
            EntityQuery query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<Enemy>()
                .WithAll<LocalTransform>()
                .Build(ref state);

            Debug.Log("SpawnNumber" + spawner.ValueRW.spawnNumber);
            ProcessSpawn(ref state, spawner);

            Debug.Log(query.CalculateEntityCount());

            // Number of enemy in the scene is all eliminated and no enemy is spawning
            if (query.CalculateEntityCount() == 0 && spawner.ValueRO.spawnNumber <= 0)
            {
                // level up
                var gameConfig = SystemAPI.GetSingleton<GameConfig>();
                gameConfig._level += 1;
                SystemAPI.SetSingleton(gameConfig);

                Debug.Log("level" + gameConfig._level);

                spawner.ValueRW.spawnNumber += gameConfig._level;
            }
        }

    }


    private void ProcessSpawn(ref SystemState state, RefRW<Spawn> spawner)
    {
        // Condition Allow a new enemy to spawn
        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime && spawner.ValueRO.spawnNumber > 0)
        {
            // Check type of spawn
            var level = SystemAPI.GetSingleton<GameConfig>()._level;
            Entity newEnemyEnity = spawner.ValueRO.enemyEntity;

            switch (level)
            {
                case 0:
                    state.EntityManager.SetComponentData(
                        newEnemyEnity,
                        LocalTransform.FromPosition(spawner.ValueRO.spawnPosition)
                        );

                    break;
                default:
                    state.EntityManager.SetComponentData(
                            newEnemyEnity,
                            // random position
                            LocalTransform.FromPosition(
                                spawner.ValueRO.spawnPosition.x,
                                    UnityEngine.Random.Range(-2f, 5f),
                                    spawner.ValueRO.spawnPosition.z)
                            );

                    break;
            }
            state.EntityManager.Instantiate(newEnemyEnity);

            // reset spawn time
            spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRW.spawnRate;
            spawner.ValueRW.spawnNumber--;

        }
    }
}
