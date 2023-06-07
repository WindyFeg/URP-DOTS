using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class RotateAuthoring : MonoBehaviour
{
    public float _speed;
    public Material _matRed;
    public Material _matGreen;
}

public class RotateBaker : Baker<RotateAuthoring>
{
    public override void Bake(RotateAuthoring authoring)
    {

        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Rotate { speed = authoring._speed });

    }
}
