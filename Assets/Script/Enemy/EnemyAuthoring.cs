using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float _speed;
    public Material _matRed;
    public Material _matGreen;
}

public class EnemyBaker : Baker<EnemyAuthoring>
{
    public override void Bake(EnemyAuthoring authoring)
    {

        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Enemy { speed = authoring._speed });

    }
}
