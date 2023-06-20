using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public struct Bullet : IComponentData
{
    public float speed;
}
[BurstCompile]
public partial struct MoveBullet : IJobEntity
{
    public float deltaTime;
    public void Execute(ref LocalTransform transform, Bullet bullet)
    {
        transform.Position.y += bullet.speed * deltaTime;
        // Rotate y
        transform.Rotation = Quaternion.Euler(0, 90 * deltaTime, 0);

    }
}