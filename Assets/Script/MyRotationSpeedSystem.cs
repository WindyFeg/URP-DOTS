using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;
using Unity.Rendering;
using System;

public partial struct MyRotationSpeedSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, speed) in SystemAPI.Query<
        RefRW<LocalTransform>,
        RefRW<Rotate>
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
        }
    }

}
