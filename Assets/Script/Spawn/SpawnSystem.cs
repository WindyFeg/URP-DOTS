using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using System;

public partial struct SpawnSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (RefRW<Spawn> spawner in SystemAPI.Query<RefRW<Spawn>>())
        {
            ProcessSpawn(ref state, spawner);
        }
    }

    private void ProcessSpawn(ref SystemState state, RefRW<Spawn> spawner)
    {
        if (spawner.ValueRO.nextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            Entity newEnemyEnity = spawner.ValueRO.enemyEntity;
            state.EntityManager.SetComponentData(
                newEnemyEnity,
                LocalTransform.FromPosition(spawner.ValueRO.spawnPosition)
                );
            state.EntityManager.Instantiate(newEnemyEnity);

            // reset spawn time
            spawner.ValueRW.nextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRW.spawnRate;
        }
    }
}
