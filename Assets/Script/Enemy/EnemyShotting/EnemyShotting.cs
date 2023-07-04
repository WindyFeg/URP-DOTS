

using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

public struct EnemyShotting : IComponentData
{
    public Entity BulletPrefab;
    public float speed;
}
