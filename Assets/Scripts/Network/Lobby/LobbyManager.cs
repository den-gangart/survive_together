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
    public UnityLobbyProvider Provider { get { return _lobbyProvider; } }

    [SerializeField] private float _lobbyHeartBeatTime;
    [SerializeField] private float _updateLobbyTime;

    private UnityLobbyProvider _lobbyProvider;
    private Timer _statusTimer;
    private Timer _updateTimer;

    protected override void OnAwake()
    {
        SetTimers();
        CreateLobbyProvider();
        base.OnAwake();
    }

    private void Update()
    {
        if(_lobbyProvider.HostLobby != null)
        {
            _statusTimer.Tick(Time.deltaTime);
            _updateTimer.Tick(Time.deltaTime);
        }
    }

    private void SetTimers()
    {
        _statusTimer = new(_lobbyHeartBeatTime);
        _statusTimer.OnTimerDone += OnHearBeatLobby;

        _updateTimer = new(_updateLobbyTime);
        _updateTimer.OnTimerDone += OnLobbyUpdate;
    }

    private void CreateLobbyProvider()
    {
        _lobbyProvider = new UnityLobbyProvider();
        _lobbyProvider.JoinedToLobby += OnJoinedToLobby;
        _lobbyProvider.LeftFromLobby += OnLeftFromLobby;
    }

    private void OnJoinedToLobby(Lobby lobby)
    {
        IsJoinedToLobby = true;
        EventSystem.Broadcast(new JoinLobbyEvent { lobby = lobby });
    }

    private void OnLeftFromLobby()
    {
        IsJoinedToLobby = false;
        EventSystem.Broadcast(new LeftLobbyEvent());
    }

    private void OnHearBeatLobby()
    {
        _lobbyProvider.HeartBeat();
        _statusTimer.Reset(_lobbyHeartBeatTime);
    }
    
    private void OnLobbyUpdate()
    {
        _lobbyProvider.UpdateLobby();
        _updateTimer.Reset(_lobbyHeartBeatTime);
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

    private void OnApplicationQuit()
    {
        if (_lobbyProvider.HostLobby != null)
            _lobbyProvider.RemoveLobby();
    }
}