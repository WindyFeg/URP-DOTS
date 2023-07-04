using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;



namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(SimulationSystemGroup))]
    public partial struct EnemyBulletCollideSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<User>();
            state.RequireForUpdate<EnemyBullet>();
            state.RequireForUpdate<SimulationSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.TempJob);

            var gameConfigEntity = SystemAPI.GetSingletonEntity<GameConfig>();
            //* Dependency is a way to make sure that the job is finished before the next job is started.
            state.Dependency = new JobCheckUserCollision
            {
                ecb = ecb,
                userLookup = state.GetComponentLookup<User>(),
                eBulletLookup = state.GetComponentLookup<EnemyBullet>(),
            }.Schedule(
                //* SimulationSingleton is a way to get the physics world.
                SystemAPI.GetSingleton<SimulationSingleton>(),
                state.Dependency
                );

            //* Complete is a way to make sure that the job is finished before the next job is started.
            state.Dependency.Complete();

            //* Playback is a way to play the command buffer. 
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    //* ITriggerEventsJob is a way to get the trigger event.
    internal struct JobCheckUserCollision : ITriggerEventsJob
    {
        //* EntityCommandBuffer is a way to create, delete, and modify entities from a job.
        public EntityCommandBuffer ecb { get; set; }

        //* When you want to passing a array of component, you need to use ComponentLookup
        public ComponentLookup<User> userLookup { get; set; }
        public ComponentLookup<EnemyBullet> eBulletLookup { get; set; }

        public void Execute(TriggerEvent triggerEvent)
        {
            var isBulletHitEnemy = (eBulletLookup.HasComponent(triggerEvent.EntityA) && userLookup.HasComponent(triggerEvent.EntityB)) || (eBulletLookup.HasComponent(triggerEvent.EntityB) && userLookup.HasComponent(triggerEvent.EntityA));

            if (isBulletHitEnemy)
            {
                if (userLookup.HasComponent(triggerEvent.EntityA))
                {
                    ecb.AddComponent(triggerEvent.EntityA, new Damage
                    {
                        Value = 5
                    });
                }
                if (userLookup.HasComponent(triggerEvent.EntityB))
                {
                    return;
                }
            }
        }
    }
}