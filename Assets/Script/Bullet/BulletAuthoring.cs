using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    public float _speed;
}

public class BulletBaker : Baker<BulletAuthoring>
{
    public override void Bake(BulletAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Bullet { speed = authoring._speed });
    }
}
