using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenuHandler : MonoBehaviour
{
    public event Action<Lobby> LobbyUpdate;

    private void OnEnable()
    {
        NetworkSystem.Instance.LobbyProvider.LobbyUpdate += OnUpdateLobby;
    }

    private void OnDisable()
    {
        NetworkSystem.Instance.LobbyProvider.LobbyUpdate -= OnUpdateLobby;
    }

    public void OnUpdateLobby(Lobby lobby)
    {
        LobbyUpdate?.Invoke(lobby);
    }

    public async void CreateLobby(LobbyParameters lobbyParams)
    {
        try
        {
            Lobby lobby = await NetworkSystem.Instance.LobbyProvider.CreateLobby(lobbyParams);
            EventSystem.Broadcast(new CreateLobbyEvent());
        } 
        catch
        {
            HandleError();
        }
    }

    public async void JoinLobbyWithCode(string code)
    {
        try
        {
            Lobby lobby = await NetworkSystem.Instance.LobbyProvider.JoinLobbyByCode(code);
            EventSystem.Broadcast(new JoinLobbyEvent());
        }
        catch
        {
            HandleError();
        }
    }

    public async void JoinLobbyWithId(string id)
    {
        try
        {
            Lobby lobby = await NetworkSystem.Instance.LobbyProvider.JoinLobbyById(id);
            EventSystem.Broadcast(new JoinLobbyEvent());       
        }
        catch
        {
            HandleError();
        }
    }

    public async void LoadLobbyList(Action<List<Lobby>> callback)
    {
        List<Lobby> lobbies = await NetworkSystem.Instance.LobbyProvider.LoadLobbyList();
        callback?.Invoke(lobbies);
    }

    public void LeaveLobby()
    {
       NetworkSystem.Instance.LobbyProvider.LeaveLobby();
    }

    public bool IsLobbyOwner() => NetworkSystem.Instance.LobbyProvider.IsOwner;
    public Lobby GetJoinedLobby() => NetworkSystem.Instance.LobbyProvider.HostLobby;

    public void CreateGameSession()
    {
        try
        {
            NetworkSystem.Instance.CreateGameSession();
        }
        catch
        {
            HandleError();
        }
    }

    private void HandleError()
    {
       
    } 
}
