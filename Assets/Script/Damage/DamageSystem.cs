
using Unity.Collections;
using Unity.Entities;

public partial struct NameSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var (dame, health, entity) in SystemAPI.Query<RefRO<Damage>, RefRW<hp>>().WithEntityAccess())
        {
            health.ValueRW.health -= dame.ValueRO.Value;
            ecb.RemoveComponent<Damage>(entity);
        }
        ecb.Playback(state.EntityManager);
    }
}