using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct Enemy : IComponentData
{
    public float speed;
}


[BurstCompile]
public partial struct EnemyMovingJob : IJobEntity
{
    public float deltaTime;
    public float speed;

    public void Execute(ref LocalTransform tf, ref Enemy enemy)
    {

        if (Mathf.Abs(tf.Position.x) > 12)
        {
            enemy.speed = -enemy.speed;

        }

        tf.Position = new float3
        {
            x = tf.Position.x + enemy.speed * deltaTime,
            y = tf.Position.y,
            z = tf.Position.z
        };

        tf.RotateY(0.3f * deltaTime);
    }

}
