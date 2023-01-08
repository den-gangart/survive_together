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
    public bool IsJoinedToLobby { get; private set; }
    public bool IsOwner { get { return _lobbyProvider.IsOwner; } }

    [SerializeField] private float _timeToCheckLobbyStatus;
    private UnityLobbyProvider _lobbyProvider;
    private Timer _timerLobbyStatus;

    protected override void OnAwake()
    {
        CreateLobbyProvider();
        base.OnAwake();
    }

    private void CreateLobbyProvider()
    {
        _lobbyProvider = new UnityLobbyProvider();
        _lobbyProvider.JoinedToLobby += OnJoinedToLobby;
        _lobbyProvider.LeftFromLobby += OnLeftFromLobby;
    }

    private void  CreateStatusLobbyTimer()
    {
        _timerLobbyStatus = new Timer(_timeToCheckLobbyStatus);
        _timerLobbyStatus.OnTimerDone += OnCheckLobbyStatus;
    }

    private void Update()
    {
        if(_lobbyProvider.HostLobby != null)
        {
            _timerLobbyStatus.Tick(Time.deltaTime);
        }
    }

    private void OnJoinedToLobby(Lobby lobby)
    {
        IsJoinedToLobby = true;
        EventSystem.Broadcast(new JoinLobbyEvent { lobby = lobby });
        CreateStatusLobbyTimer();
    }

    private void OnLeftFromLobby()
    {
        IsJoinedToLobby = false;
        EventSystem.Broadcast(new LeftLobbyEvent());
    }

    private void OnCheckLobbyStatus()
    {
        _timerLobbyStatus.Reset(_timeToCheckLobbyStatus);
        _lobbyProvider.CheckLobbyStatus();
    }
    public void CreateLobby(LobbyParameters lobbyParameters)
    {
        _lobbyProvider.CreateLobby(lobbyParameters);
    }

    public void JoinToLobbyById(string id)
    {
        _lobbyProvider.JoinLobbyById(id);
    }

    public void JoinToLobbyWithCode(string code)
    {
        _lobbyProvider.JoinLobbyByCode(code);
    }
}