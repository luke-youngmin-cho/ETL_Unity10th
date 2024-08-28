using Fusion;
using System.Collections;
using UnityEngine;
public class InGameClientState : NetworkBehaviour
{
    public bool isReady
    {
        get => _isReady;
        set
        {
            if (_isReady == value) 
                return;

            RPC_ChangeIsReady(value);
        }
    }
    [SerializeField]private bool _isReady;

    [Rpc(RpcSources.All, RpcTargets.All)]
    void RPC_ChangeIsReady(bool isReady)
    {
        _isReady = isReady;
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Runner != null);
        Runner.GetComponent<GameManager>().inGameClientStates.TryAdd(Runner.LocalPlayer, this);
    }
}
