using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour, INetworkRunnerCallbacks
{
    public Dictionary<PlayerRef, InGameClientState> inGameClientStates = new Dictionary<PlayerRef, InGameClientState>();
    public Dictionary<PlayerRef, NetworkObject> clientNetworkObjects = new Dictionary<PlayerRef, NetworkObject>();
    [SerializeField] NetworkPrefabRef _inGameClientStatePrefab;
    [SerializeField] NetworkPrefabRef _clientCharactor;
    public int localPlayerID;
    

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
            // 클라이언트 접속 감지
            if (runner.LocalPlayer != player)
            {
                if (inGameClientStates.TryGetValue(player, out InGameClientState inGameClientState) == false &&
                    clientNetworkObjects.TryGetValue(player, out NetworkObject inGameClientObject) == false)
                {
                    NetworkObject networkObject = runner.Spawn(_inGameClientStatePrefab);
                    networkObject.gameObject.name = $"InGameClientState - {player.PlayerId}";

                    Vector3 randomPositionRel = UnityEngine.Random.insideUnitSphere * 10f;
                    randomPositionRel.y = 0f;
                    Vector3 spawnPosition = transform.position + randomPositionRel;
                    runner.Spawn(_clientCharactor, spawnPosition, Quaternion.identity, player);
                }
            }
        }

        if (runner.IsClient)
        {
            // 내 클라언트 세션만들어졌을때
            if (runner.LocalPlayer == player)
            {
                localPlayerID = player.PlayerId;
                InitializeLocalClientContent(runner, runner.LocalPlayer);
            }
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
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

    private void InitializeLocalClientContent(NetworkRunner client, PlayerRef localPlayerRef)
    {
        UI_InGameScreen uI_InGameScreen = Instantiate(Resources.Load<UI_InGameScreen>($"UI/{nameof(UI_InGameScreen)}"));
        uI_InGameScreen.Initialize(client, localPlayerRef);
        uI_InGameScreen.name = $"{nameof(UI_InGameScreen)} - {localPlayerRef.PlayerId}";
    }
}
