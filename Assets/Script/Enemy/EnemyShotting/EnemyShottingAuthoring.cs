using Unity.Entities;
using UnityEngine;
public class EnemyShottingAuthoring : MonoBehaviour
{
    public GameObject EnemyBulletPrefab;

    public float speed;
}

//* This is a way to add component to the entity.
public class EnemyShottingBaker : Baker<EnemyShottingAuthoring>
{
    public override void Bake(EnemyShottingAuthoring authoring)
    {
        //* GetEntity is a way to get the entity from the entity manager.
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        //* AddComponent is a way to add component to the entity.
        AddComponent(entity, new EnemyShotting
        {
            BulletPrefab = GetEntity(authoring.EnemyBulletPrefab, TransformUsageFlags.Dynamic),
            speed = authoring.speed
        });
    }
}