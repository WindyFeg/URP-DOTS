using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class SpawnAuthoring : MonoBehaviour
{
    public GameObject _entityPrefab;
    public Vector3 _spawnPosition;

    public bool _canSpawn;

    public float _nextSpawnTime;
    public float _spawnRate;

}

public class SpawnBaker : Baker<SpawnAuthoring>
{
    public override void Bake(SpawnAuthoring authoring)
    {
        // authoring._spawnPosition.y = Random.Range(-2f, 5);

        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Spawn
        {
            spawnPosition = authoring._spawnPosition,
            canSpawn = authoring._canSpawn,
            enemyEntity = GetEntity(authoring._entityPrefab),
            nextSpawnTime = authoring._nextSpawnTime,
            spawnRate = authoring._spawnRate
        });


    }
}
