
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;

public partial struct MatAndMesSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        // Assign material

    }
    public void OnUpdate(ref SystemState state)
    {
        foreach (var (matAndMes, matmeshinfo, entity) in SystemAPI.Query<RefRO<MatAndMes>, RefRW<MaterialMeshInfo>>().WithEntityAccess())
        {
            matmeshinfo.ValueRW.MaterialID = matAndMes.ValueRO.materialID;
            matmeshinfo.ValueRW.MeshID = matAndMes.ValueRO.meshID;
        }
    }
}