using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCheckTimers : MonoBehaviour
{
    [SerializeField] private float _lobbyHeartBeatTime;
    [SerializeField] private float _updateLobbyTime;

    private Timer _statusTimer;
    private Timer _updateTimer;

    private UnityLobbyProvider _lobbyProvider => NetworkSystem.Instance.LobbyProvider;

    private void Start()
    {
        _statusTimer = new(_lobbyHeartBeatTime);
        _statusTimer.OnTimerDone += _lobbyProvider.HeartBeat;

        _updateTimer = new(_updateLobbyTime);
        _updateTimer.OnTimerDone += _lobbyProvider.UpdateLobby;
    }

    private void Update()
    {
        if (_lobbyProvider.IsJoinedToLobby)
        {
            if(_lobbyProvider.IsOwner)
            {
                _statusTimer.Tick(Time.deltaTime);
            }
            _updateTimer.Tick(Time.deltaTime);
        }
    }
}
