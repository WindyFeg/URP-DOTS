using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Spawn : IComponentData
{
    public Entity enemyEntity;
    public float3 spawnPosition;

    public bool canSpawn;

    public float nextSpawnTime;
    public float spawnRate;

}
