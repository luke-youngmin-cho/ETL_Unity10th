using Fusion;
using Fusion.Addons.KCC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KCC))]
public class ClientNetworkMovement : NetworkBehaviour
{
    private KCC _kcc;
    private Vector3 _velocity;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Runner != null);
        Runner.GetComponent<GameManager>().clientNetworkObjects.TryAdd(Runner.LocalPlayer, GetComponent<NetworkObject>());
    }

    public override void Spawned()
    {
        base.Spawned();

        _kcc = GetComponent<KCC>();
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (Runner.TryGetInputForPlayer(Object.InputAuthority, out NetworkInputData inputData))
        {
            _velocity = new Vector3(inputData.move.x, 0f, inputData.move.y);
            _kcc.AddLookRotation(inputData.rotationDelta);
            _kcc.SetInputDirection(_kcc.Data.TransformRotation * _velocity);
        }
    }
}
