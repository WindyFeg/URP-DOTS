using Unity.Entities;
using UnityEngine;

public class EnemyBulletAuthoring : MonoBehaviour
{
    public float _speed;
}

//* This is a way to add component to the entity.
public class EnemyBulletBaker : Baker<EnemyBulletAuthoring>
{
    public override void Bake(EnemyBulletAuthoring authoring)
    {
        //* GetEntity is a way to get the entity from the entity manager.
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        //* AddComponent is a way to add component to the entity.
        AddComponent(entity, new EnemyBullet
        {
            speed = authoring._speed
        });
    }
}