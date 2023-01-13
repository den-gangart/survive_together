using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkSceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        EventSystem.AddEventListener<StartGameSessionEvent>(OnGameStarted);
    }

    private void OnDisable()
    {
        EventSystem.RemoveEventListener<StartGameSessionEvent>(OnGameStarted);
    }

    private void OnGameStarted(StartGameSessionEvent e)
    {
        if(NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(SceneNames.GAME_FIELD_SCENE, LoadSceneMode.Single);
        }
    }
}
