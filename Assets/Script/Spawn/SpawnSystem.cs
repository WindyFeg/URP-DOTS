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

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {

        foreach (RefRW<Spawn> spawner in SystemAPI.Query<RefRW<Spawn>>())
        {
            EntityQuery query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<Enemy>()
                .WithAll<LocalTransform>()
                .Build(ref state);

            var numberOfEnemyInScene = query.CalculateEntityCount();
            // Debug.Log(numberOfEnemyInScene);
            if (spawner.ValueRO.canSpawn && (numberOfEnemyInScene == 0))
            {
                // Spawn
                ProcessSpawn(ref state, spawner);
                //Level up
                LevelUp(ref state, spawner, query);
            }
        }

    }

    private void LevelUp(ref SystemState state, RefRW<Spawn> spawner, EntityQuery query)
    {
        // level up
        var gameConfig = SystemAPI.GetSingleton<GameConfig>();
        gameConfig._level += 1;
        SystemAPI.SetSingleton(gameConfig);
    }

    private float3 Vector3ToFloat3(Vector3 vector3)
    {
        return new float3(vector3.x, vector3.y, vector3.z);
    }

    private void ProcessSpawn(ref SystemState state, RefRW<Spawn> spawner)
    {
        // Condition Allow a new enemy to spawn
        // if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        // Check type of spawn
        var level = SystemAPI.GetSingleton<GameConfig>()._level;
        Entity newEnemyEntity = spawner.ValueRO.enemyEntity;

        switch (level)
        {
            case 0:
                for (int i = -5; i <= 5; i++)
                {
                    var pX = i / 2.5f;
                    float3 position = new float3
                    {
                        x = pX,
                        y = pX * pX,
                        z = 0
                    };
                    state.EntityManager.SetComponentData(
                        newEnemyEntity,
                        LocalTransform.FromPosition(position).WithScale(0.5f)
                        );
                    state.EntityManager.Instantiate(newEnemyEntity);
                }

                break;

            case 1:
                for (int i = -15; i <= 15; i++)
                {
                    var pX = 16 * Mathf.Sin(i) * Mathf.Sin(i) * Mathf.Sin(i);
                    var pY = 13 * Mathf.Cos(i) - 5 * Mathf.Cos(2 * i) - 2 * Mathf.Cos(3 * i) - Mathf.Cos(4 * i);
                    float3 position = new float3
                    {
                        x = pX / 5,
                        y = pY / 5 + 0.5f,
                        z = 0
                    };
                    state.EntityManager.SetComponentData(
                        newEnemyEntity,
                        LocalTransform.FromPosition(position).WithScale(0.5f)
                        );
                    state.EntityManager.Instantiate(newEnemyEntity);
                }
                break;
            case 2:
                for (int i = -1; i <= 1; i++)
                {
                    var pX = i;
                    var pY = Mathf.Pow(10 - Mathf.Pow(pX, 2 / 3), Mathf.Pow(pX, 3 / 2));
                    float3 position = new float3
                    {
                        x = pX,
                        y = pY,
                        z = 0
                    };
                    state.EntityManager.SetComponentData(
                        newEnemyEntity,
                        LocalTransform.FromPosition(position).WithScale(0.5f)
                        );
                    state.EntityManager.Instantiate(newEnemyEntity);
                }
                break;
            default:
                state.EntityManager.SetComponentData(
                        newEnemyEntity,
                        // random position
                        LocalTransform.FromPosition(
                            spawner.ValueRO.spawnPosition.x,
                                UnityEngine.Random.Range(-2f, 5f),
                                spawner.ValueRO.spawnPosition.z)
                        );

                break;
        }

        // reset spawn time
        // spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRW.spawnRate;
        // spawner.ValueRW.spawnNumber--;

    }
}
