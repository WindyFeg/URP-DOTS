using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSystem : ISystem
{
    float deltaTime;
    public EntityQuery bulletQuery;
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        deltaTime = SystemAPI.Time.DeltaTime;
        bulletQuery = new EntityQueryBuilder(Allocator.Temp)
    .WithAllRW<Bullet>()
    .WithAll<LocalTransform>()
    .WithOptions(EntityQueryOptions.FilterWriteGroup)
    .Build(ref state);
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //! Query
        // MoveBullet(ref state);

        //! Job System
        JobMoveBullet(ref state);
    }

    private void JobMoveBullet(ref SystemState state)
    {
        var bulletJob = new MoveBullet
        {
            deltaTime = SystemAPI.Time.DeltaTime
        };
        bulletJob.ScheduleParallel(bulletQuery);
    }

    private void MoveBullet(ref SystemState state)
    {
        foreach (var (transform, speed, entity) in SystemAPI.Query<
                RefRW<LocalTransform>,
                RefRW<Bullet>
                >().WithEntityAccess())
        {
            transform.ValueRW.Position.y += speed.ValueRO.speed * deltaTime;

            // Rotate y
            transform.ValueRW.Rotation = Quaternion.Euler(0, 90 * deltaTime, 0);
        }
    }

}
