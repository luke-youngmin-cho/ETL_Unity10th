using Fusion;
using Fusion.Addons.KCC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KCC))]
public class ClientNetworkMovement : NetworkBehaviour
{
    private KCC _kcc;
    [Networked]
    public float _speed { get; set; } = 1;
    [Networked]
    public Vector3 _velocity { get; set; }
    [Networked]
    public bool _doAttack { get; set; }
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => Runner != null);
        Runner.GetComponent<GameManager>().clientNetworkObjects.TryAdd(Runner.LocalPlayer, GetComponent<NetworkObject>());
    }

    public override void Spawned()
    {
        base.Spawned();

        _kcc = GetComponent<KCC>();

        if (HasInputAuthority)
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerMine");
            transform.Find("KCCCollider").gameObject.layer = gameObject.layer;
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (Runner.TryGetInputForPlayer(Object.InputAuthority, out NetworkInputData inputData))
        {
            _velocity = new Vector3(inputData.move.x, 0f, inputData.move.y).normalized * _speed;
            _kcc.AddLookRotation(inputData.rotationDelta);
            _kcc.SetInputDirection(_kcc.Data.TransformRotation * _velocity);

            _animator.SetFloat("speed", _velocity.magnitude);

            _doAttack = inputData.fire;
        }
    }

    public override void Render()
    {
        base.Render();
        _animator.SetFloat("speed", _velocity.magnitude);
        _animator.SetBool("doAttack", _doAttack);
    }
}
