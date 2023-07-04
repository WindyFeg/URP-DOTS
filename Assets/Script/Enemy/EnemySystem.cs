using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Jobs;
using System;
using UnityEngine;
using Unity.Mathematics;


[RequireMatchingQueriesForUpdate]
public partial struct EnemySystem : ISystem
{
    float deltaTime;
    EntityQuery enemyQuery;

    public void OnCreate(ref SystemState state)
    {
        //! Only run this system when StartCommand is called
        state.RequireForUpdate<StartCommand>();

        deltaTime = SystemAPI.Time.DeltaTime;

        //* Enemy Query
        enemyQuery = new EntityQueryBuilder(Allocator.Temp)
            .WithAllRW<Enemy>()
            .WithAll<LocalTransform>()
            .WithOptions(EntityQueryOptions.FilterWriteGroup)
            .Build(ref state);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //! USING QUERY
        // QueryToMoveEnemy(ref state);

        // !USING JOB SYSTEM
        JobSystemToMoveEnemy(ref state);
    }

    private void JobSystemToMoveEnemy(ref SystemState state)
    {
        var _job = new EnemyMovingJob
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        _job.ScheduleParallel(enemyQuery);

    }

    private void QueryToMoveEnemy(ref SystemState state)
    {
        foreach (var (transform, speed) in SystemAPI.Query<
        RefRW<LocalTransform>,
        RefRW<Enemy>
        >())
        {
            if (Mathf.Abs(transform.ValueRW.Position.x) > 12)
            {
                speed.ValueRW.speed = -speed.ValueRO.speed;

            }

            transform.ValueRW.Position = new float3
            {
                x = transform.ValueRW.Position.x + speed.ValueRO.speed * deltaTime,
                y = transform.ValueRW.Position.y,
                z = transform.ValueRW.Position.z
            };

            transform.ValueRW.RotateY(1 * deltaTime);
        }
    }
}
