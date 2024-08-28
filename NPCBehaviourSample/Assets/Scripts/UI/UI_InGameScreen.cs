using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGameScreen : MonoBehaviour
{
    [SerializeField] Toggle _ready;


    public void Initialize(NetworkRunner runner, PlayerRef localPlayerRef)
    {
        StartCoroutine(C_Initialize(runner, localPlayerRef));
    }

    IEnumerator C_Initialize(NetworkRunner runner, PlayerRef localPlayerRef)
    {
        GameManager gameManager = runner.GetComponent<GameManager>();

        yield return new WaitUntil(() => gameManager.inGameClientStates.ContainsKey(localPlayerRef));

        _ready.onValueChanged.AddListener(isReady =>
        {
            gameManager.inGameClientStates[localPlayerRef].isReady = isReady;
        });
    }
}
