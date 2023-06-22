using Unity.Entities;
using UnityEngine;

public class GameConfigAuthoring : MonoBehaviour
{
    public float level;

    public float score;
}

//* This is a way to add component to the entity.
public class GameConfigBaker : Baker<GameConfigAuthoring>
{
    public override void Bake(GameConfigAuthoring authoring)
    {
        //* GetEntity is a way to get the entity from the entity manager.
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        //* AddComponent is a way to add component to the entity.
        AddComponent(entity, new GameConfig
        {
            _level = authoring.level,
            _score = authoring.score
        });
    }
}