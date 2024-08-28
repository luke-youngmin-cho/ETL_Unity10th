using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void InitializeApplication()
    {
        SceneManager.LoadScene("Main");
        GameObject gameObject = new GameObject("BootStrap");
        gameObject.AddComponent<Bootstrap>()
                  ._networkRunnerPrefab = Resources.Load<NetworkRunner>("Network/NetworkRunnerBase");
        DontDestroyOnLoad(gameObject);
    }

    private NetworkRunner _networkRunnerPrefab;
    private NetworkRunner _server;
    private List<NetworkRunner> _clients = new List<NetworkRunner>();

    /// <summary>
    /// Network 이용자 모드 설정 
    /// </summary>
    /// <param name="gameMode"></param>
    public async void StartGame(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.Server:
                {
                    _server = Instantiate(_networkRunnerPrefab);
                    _server.ProvideInput = true;
                    await StartRunnerAsync(_server, gameMode);
                    Debug.Log($"[{name}] : Started with server...");
                }
                break;
            case GameMode.Host:
                break;
            case GameMode.Client:
                {
                    var client = Instantiate(_networkRunnerPrefab);
                    client.ProvideInput = true;
                    await StartRunnerAsync(client, gameMode);
                    Debug.Log($"[{name}] : Started with client...");
                    _clients.Add(client);
                }
                break;
            default:
                break;
        }
    }

    async Task<StartGameResult> StartRunnerAsync(NetworkRunner runner, GameMode gameMode)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Playground.unity");
        SceneRef sceneRef = SceneRef.FromIndex(sceneIndex);
        NetworkSceneInfo networkSceneInfo = new NetworkSceneInfo();
        networkSceneInfo.AddSceneRef(sceneRef, LoadSceneMode.Additive);
        StartGameArgs startGameArgs = new StartGameArgs
        {
            GameMode = gameMode,
            SessionName = "Test",
            Scene = networkSceneInfo,
            SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>(),
        };
        return await runner.StartGame(startGameArgs);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 40), "Server"))
            StartGame(GameMode.Server);

        if (GUI.Button(new Rect(0, 40, 200, 40), "Client"))
            StartGame(GameMode.Client);
    }
}
