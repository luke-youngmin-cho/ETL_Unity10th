using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerController : NetworkBehaviour
{
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (GetInput(out NetworkInputData inputData))
        {
            transform.position += new Vector3(inputData.move.x, 0, inputData.move.y);
        }
    }
}
