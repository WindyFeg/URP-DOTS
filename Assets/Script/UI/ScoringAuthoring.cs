
using Unity.Entities;
using UnityEngine;

public class ScoreAuthoring : MonoBehaviour
{
    public float _score;
}

//* This is a way to add component to the entity.
public class ScoringBaker : Baker<ScoreAuthoring>
{
    public override void Bake(ScoreAuthoring authoring)
    {
        //* GetEntity is a way to get the entity from the entity manager.
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        //* AddComponent is a way to add component to the entity.
        AddComponent(entity, new Scoring
        {
            // assign component data here
            score = authoring._score
        });
    }
}