using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NetworkRunner))]
public class Spawner : MonoBehaviour, INetworkRunnerCallbacks, InputActions.IPlayerActions
{
    [SerializeField] float _spawnRadius = 10f;
    [SerializeField] NetworkPrefabRef _playerPrefabRef;
    [SerializeField] NetworkPrefabRef _enemyPrefabRef;
    Dictionary<PlayerRef, NetworkObject> _playerObjects = new Dictionary<PlayerRef, NetworkObject>();
    List<NetworkObject> _enemies = new List<NetworkObject>();

    [Header("Input")]
    [SerializeField]Vector2 _moveValueCached;
    [SerializeField]bool _fireValueCached;
    InputActions _inputActions;

    private void Awake()
    {
        _inputActions = new InputActions();
        _inputActions.Player.AddCallbacks(this);
        _inputActions.Enable();
        _inputActions.Player.Enable();
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData inputData = new NetworkInputData();
        inputData.move = _moveValueCached;
        inputData.fire = _fireValueCached;
        input.Set(inputData);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Vector3 randomPositionRel = UnityEngine.Random.insideUnitSphere * _spawnRadius;
            randomPositionRel.y = 0f;
            Vector3 spawnPosition = transform.position + randomPositionRel;
            NetworkObject networkObject = runner.Spawn(_playerPrefabRef, spawnPosition, Quaternion.identity, player);
            _playerObjects.Add(player, networkObject);
            Debug.Log($"Joined Server. Start spawn enemies...");

            for (int i = 0; i < 10; i++)
            {
                randomPositionRel = UnityEngine.Random.insideUnitSphere * _spawnRadius;
                randomPositionRel.y = 0f;
                spawnPosition = transform.position + randomPositionRel;
                networkObject = runner.Spawn(_enemyPrefabRef, spawnPosition, Quaternion.identity, player);
                _enemies.Add(networkObject);
            }
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            runner.Despawn(_playerObjects[player]);
            _playerObjects.Remove(player);
        }
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveValueCached = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        _fireValueCached = context.ReadValueAsButton();
    }
}
