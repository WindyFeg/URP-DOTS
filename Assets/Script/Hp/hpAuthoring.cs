using Unity.Entities;
using UnityEngine;


public class hpAuthoring : MonoBehaviour
{
    public float _hp;

    public float _armor;
}

public class hpBaker : Baker<hpAuthoring>
{
    public override void Bake(hpAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new hp
        {
            health = authoring._hp,
            armor = authoring._armor
        });
    }
}