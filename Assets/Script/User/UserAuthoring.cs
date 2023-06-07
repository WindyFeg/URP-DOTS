using Unity.Entities;
using UnityEngine;

public class UserAuthoring : MonoBehaviour
{
    public float _health;
    public GameObject _bulletPrefab;
}

public class UserBaker : Baker<UserAuthoring>
{
    public override void Bake(UserAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new User
        {
            health = authoring._health,
            bulletPrefab = GetEntity(authoring._bulletPrefab, TransformUsageFlags.Dynamic)
        });
    }
}
