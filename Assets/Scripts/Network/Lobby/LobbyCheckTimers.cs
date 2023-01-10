using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LobbyManager))]
public class LobbyCheckTimers : MonoBehaviour
{
    [SerializeField] private float _lobbyHeartBeatTime;
    [SerializeField] private float _updateLobbyTime;

    private Timer _statusTimer;
    private Timer _updateTimer;

    private LobbyManager _lobbyManager;

    private void Start()
    {
        _lobbyManager = GetComponent<LobbyManager>();

        _statusTimer = new(_lobbyHeartBeatTime);
        _statusTimer.OnTimerDone += _lobbyManager.OnHearBeatLobby;

        _updateTimer = new(_updateLobbyTime);
        _updateTimer.OnTimerDone += _lobbyManager.OnLobbyUpdate;
    }

    private void Update()
    {
        if (_lobbyManager.IsJoinedToLobby)
        {
            _statusTimer.Tick(Time.deltaTime);
            _updateTimer.Tick(Time.deltaTime);
        }
    }
}
