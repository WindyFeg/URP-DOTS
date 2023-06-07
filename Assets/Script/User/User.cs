using Unity.Entities;

public struct User : IComponentData
{
    public float health;
    public Entity bulletPrefab;
}
