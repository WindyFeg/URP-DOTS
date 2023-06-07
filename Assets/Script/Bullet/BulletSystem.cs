using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct BulletSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        // EntityQuery bulletEntityQuery = state.EntityManager.CreateEntityQuery(typeof(Bullet));

        // Bullet bullet = bulletEntityQuery.GetSingleton<Bullet>();

        // int spawnAmount = 2;

        // if (bulletEntityQuery.CalculateEntityCount() < spawnAmount)
        // {
        //     EntityManager.Instantiate(bullet.bulletPrefab);
        // }


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
