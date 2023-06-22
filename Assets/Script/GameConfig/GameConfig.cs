using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct GameConfig : IComponentData
{
    public float _level;
    public float _score;
}
