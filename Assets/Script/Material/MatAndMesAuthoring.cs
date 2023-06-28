using Unity.Entities;
using Unity.Rendering;
using UnityEngine;


public class MatAndMesAuthoring : MonoBehaviour
{
    public Mesh _mesh;
    public Material _material;
}

public class MatAndMesBaker : Baker<MatAndMesAuthoring>
{
    public override void Bake(MatAndMesAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        var hybirdRender = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<Entities​Graphics​System>();

        AddComponent(entity, new MatAndMes
        {
            meshID = hybirdRender.RegisterMesh(authoring._mesh),
            materialID = hybirdRender.RegisterMaterial(authoring._material)
        });
    }
}