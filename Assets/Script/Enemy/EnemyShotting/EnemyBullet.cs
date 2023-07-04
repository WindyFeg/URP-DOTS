using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

public struct EnemyBullet : IComponentData
{
    public float speed;
}
[BurstCompile]
public partial struct EnemyMoveBullet : IJobEntity
{
    public float deltaTime;
    public void Execute(ref LocalTransform transform, EnemyBullet bullet)
    {
        transform.Position.y -= bullet.speed * deltaTime;
    }
}