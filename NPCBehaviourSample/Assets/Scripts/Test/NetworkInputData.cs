using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector2 move;
    public Vector2 rotationDelta;
    public bool fire;
}
