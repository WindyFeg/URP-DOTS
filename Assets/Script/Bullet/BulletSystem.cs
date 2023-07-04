using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSystem : ISystem
{
    float deltaTime;
    public EntityQuery bulletQuery;
    public EntityQuery enemyBulletQuery;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        //! Only run this system when StartCommand is called
        state.RequireForUpdate<StartCommand>();


        deltaTime = SystemAPI.Time.DeltaTime;
        bulletQuery = new EntityQueryBuilder(Allocator.Temp)
        .WithAllRW<Bullet>()
        .WithAll<LocalTransform>()
        .WithOptions(EntityQueryOptions.FilterWriteGroup)
        .Build(ref state);

        enemyBulletQuery = new EntityQueryBuilder(Allocator.Temp)
        .WithAllRW<EnemyBullet>()
        .WithAll<LocalTransform>()
        .WithOptions(EntityQueryOptions.FilterWriteGroup)
        .Build(ref state);

    }

    // [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //! Query
        MoveEnemyBullet(ref state);

        //! Job System
        JobMoveBullet(ref state);
        // complete the job
        state.Dependency.Complete();

        //if bullet above 10, destroy it
        DestroyBullet(ref state);

    }

    private void DestroyBullet(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);

        foreach (var (bullet, tf, entity) in SystemAPI.Query<RefRO<Bullet>, RefRO<LocalTransform>>().WithEntityAccess())
        {
            if (tf.ValueRO.Position.y > 10)
            {
                ecb.DestroyEntity(entity);
            }
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }

    private void JobMoveBullet(ref SystemState state)
    {
        var bulletJob = new MoveBullet
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        //* ScheduleParallel is a way to run the job in parallel.
        bulletJob.ScheduleParallel(bulletQuery);
    }

    private void MoveEnemyBullet(ref SystemState state)
    {
        var bulletJob = new EnemyMoveBullet
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        //* ScheduleParallel is a way to run the job in parallel.
        bulletJob.ScheduleParallel(enemyBulletQuery);
    }

}

