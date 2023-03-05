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
        EventSystem.AddEventListener<SignInEvent>(OnSignedIn);
        EventSystem.AddEventListener<SignOutEvent>(OnSignedOut);
    }

    private void OnDisable()
    {
        EventSystem.RemoveEventListener<StartGameSessionEvent>(OnGameStarted);
        EventSystem.AddEventListener<SignInEvent>(OnSignedIn);
        EventSystem.AddEventListener<SignOutEvent>(OnSignedOut);
    }

    private void OnGameStarted(StartGameSessionEvent e)
    {
        if(NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene(SceneNames.GAME_FIELD_SCENE, LoadSceneMode.Single);
        }
    }

    private void OnSignedIn(SignInEvent e)
    {
        SceneManager.LoadScene(SceneNames.MENU_SCENE);
    }

    private void OnSignedOut(SignOutEvent e)
    {
        SceneManager.LoadScene(SceneNames.AUTHORIZATION_SCENE);
    }
}
