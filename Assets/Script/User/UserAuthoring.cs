using Unity.Entities;
using UnityEngine;

public class UserAuthoring : MonoBehaviour
{
    public GameObject _bulletPrefab;
}

public class UserBaker : Baker<UserAuthoring>
{
    public override void Bake(UserAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new User
        {
            bulletPrefab = GetEntity(authoring._bulletPrefab, TransformUsageFlags.Dynamic)
        });
    }
}
