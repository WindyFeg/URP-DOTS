using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

public partial struct EnemySystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

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
