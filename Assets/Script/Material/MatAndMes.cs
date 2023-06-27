using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Rendering;

public struct MatAndMes : IComponentData
{
    // create mesh
    public BatchMaterialID materialID;
    public BatchMeshID meshID;
}
