using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Entities;

// Job adding two floating point values together
public struct MyJob : IComponentData
{
    public float Value;

}