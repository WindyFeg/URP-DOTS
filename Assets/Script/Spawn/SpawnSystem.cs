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
    EntityQuery componentQuery;
    EntityQuery query;
    float CurrentLevel;

    [BurstCompile]
    public partial struct SpawnJob : IJobEntity
    {
        // variable
        public EntityCommandBuffer.ParallelWriter ecb;
        public float numberOfEnemyInScene;
        public float level;

        public void Execute(ref LocalTransform tf, ref Spawn spawner)
        {
            // action
            if (spawner.canSpawn && (numberOfEnemyInScene == 0))
            {
                ProcessSpawn(ref ecb, spawner);
            }
        }


        private void ProcessSpawn(ref EntityCommandBuffer.ParallelWriter ecb, Spawn spawner)
        {
            // var level = SystemAPI.GetSingleton<GameConfig>()._level;
            Entity newEnemyEntity = spawner.enemyEntity;
            // var level = 0;
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
                        ecb.SetComponent(i,
                            newEnemyEntity,
                            LocalTransform.FromPosition(position).WithScale(0.5f)
                            );
                        ecb.Instantiate(i, newEnemyEntity);
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
                        ecb.SetComponent(i,
                            newEnemyEntity,
                            LocalTransform.FromPosition(position).WithScale(0.5f)
                            );
                        ecb.Instantiate(i, newEnemyEntity);
                    }
                    break;
                default:
                    break;
            }
        }




        private float3 Vector3ToFloat3(Vector3 vector3)
        {
            return new float3(vector3.x, vector3.y, vector3.z);
        }
    }

    public void OnCreate(ref SystemState state)
    {
        componentQuery = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<LocalTransform>()
            .WithAll<Spawn>()
            .Build(ref state);

        query = new EntityQueryBuilder(Allocator.Temp)
                    .WithAll<Enemy>()
                    .WithAll<LocalTransform>()
                    .Build(ref state);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var numberOfEnemyInScene = query.CalculateEntityCount();

        if (numberOfEnemyInScene == 0)
        {
            CurrentLevel = LevelUp(ref state);
        }
        state.Dependency = new SpawnJob
        {
            ecb = ecb.AsParallelWriter(),
            numberOfEnemyInScene = numberOfEnemyInScene,
            level = CurrentLevel
        }.ScheduleParallel(state.Dependency);

        state.Dependency.Complete();
        ecb.Playback(state.EntityManager);
        ecb.Dispose();

    }

    private float LevelUp(ref SystemState state)
    {
        // level up
        var gameConfig = SystemAPI.GetSingleton<GameConfig>();
        gameConfig._level += 1;
        SystemAPI.SetSingleton(gameConfig);
        return gameConfig._level;
    }


}
