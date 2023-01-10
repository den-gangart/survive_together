using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System;

public class LobbyManager : Singleton<LobbyManager>
{
    public bool IsJoinedToLobby { get { return _lobbyProvider.HostLobby != null; } }
    public bool IsOwner { get { return _lobbyProvider.IsOwner; } }

    public event Action<Lobby> JoinedToLobby;
    public event Action<Lobby> LobbyUpdate;
    public event Action<List<Lobby>> LoadedLobbyList;

    private UnityLobbyProvider _lobbyProvider;

    protected override void OnAwake()
    {
        CreateLobbyProvider();
        base.OnAwake();
    }

    private void CreateLobbyProvider()
    {
        _lobbyProvider = new UnityLobbyProvider(new UnityLobbyOptions());
    }

    private void OnJoinedToLobby(Lobby lobby)
    {
        JoinedToLobby?.Invoke(lobby);
        EventSystem.Broadcast(new JoinLobbyEvent { lobby = lobby });
    }

    private void OnLeftFromLobby()
    {
        EventSystem.Broadcast(new LeftLobbyEvent());
    }

    public void OnHearBeatLobby()
    {
        _lobbyProvider.HeartBeat();
    }
    
    public void OnLobbyUpdate()
    {
        _lobbyProvider.UpdateLobby(LobbyUpdate);
    }

    public void CreateLobby(LobbyParameters lobbyParameters)
    {
        _lobbyProvider.CreateLobby(lobbyParameters, OnJoinedToLobby);
    }

    public void JoinToLobbyById(string id)
    {
        _lobbyProvider.JoinLobbyById(id, OnJoinedToLobby);
    }

    public void JoinToLobbyWithCode(string code)
    {
        _lobbyProvider.JoinLobbyByCode(code, OnJoinedToLobby);
    }

    public void LoadLobbyList()
    {
        _lobbyProvider.LoadLobbyList(LoadedLobbyList);
    }

    public void LeaveLobby()
    {
        _lobbyProvider.LeaveLobby(OnLeftFromLobby);
    }

    private void OnApplicationQuit()
    {
        if (_lobbyProvider.HostLobby != null && _lobbyProvider.IsOwner)
            _lobbyProvider.RemoveLobby();
    }
}