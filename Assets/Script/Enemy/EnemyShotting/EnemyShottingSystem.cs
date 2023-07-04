
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct EnemyShottingSystem : ISystem
{
    public float time;
    private float deltaTime;
    private EntityQuery bulletQuery;

    public void OnCreate(ref SystemState state)
    {
        time = 0;

        deltaTime = SystemAPI.Time.DeltaTime;
        bulletQuery = new EntityQueryBuilder(Allocator.Temp)
    .WithAllRW<EnemyBullet>()
    .WithAll<LocalTransform>()
    .WithOptions(EntityQueryOptions.FilterWriteGroup)
    .Build(ref state);
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        if (time < SystemAPI.Time.ElapsedTime)
        {
            foreach (var (enemy, bullet, trans, entity) in SystemAPI.Query<RefRO<Enemy>, RefRO<EnemyShotting>, RefRO<LocalTransform>>().WithEntityAccess())
            {
                var _bullet = ecb.Instantiate(bullet.ValueRO.BulletPrefab);
                ecb.AddComponent(_bullet, new EnemyBullet
                {
                    speed = bullet.ValueRO.speed
                });
                ecb.SetComponent(_bullet, new LocalTransform
                {
                    Position = trans.ValueRO.Position,
                    Scale = 0.5f
                });
            }


            ecb.Playback(state.EntityManager);
            ecb.Dispose();

            time += 2;
        }

    }



}