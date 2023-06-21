using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    public float _speed;
    public float _damage;
}

//* This is a way to add component to the entity.
public class BulletBaker : Baker<BulletAuthoring>
{
    public override void Bake(BulletAuthoring authoring)
    {
        //* GetEntity is a way to get the entity from the entity manager.
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        //* AddComponent is a way to add component to the entity.
        AddComponent(entity, new Bullet
        {
            speed = authoring._speed,
            damage = authoring._damage
        });
    }
}

