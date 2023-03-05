using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class NetworkSystem : Singleton<NetworkSystem>
{
    private AuthenticationManager _authenticationManager;
    private UnityLobbyProvider _lobbyProvider;
    private RelayMultiplayerProvider _multiplayerProvider;

    public AuthenticationManager AuthenticationManager { get { return _authenticationManager; } }
    public UnityLobbyProvider LobbyProvider { get { return _lobbyProvider; } }
    public IMultiplayerProviderWithCode MultiplayerProvider { get { return _multiplayerProvider; } }

    protected async override void OnAwake()
    {
        await UnityServices.InitializeAsync();

        _authenticationManager = new AuthenticationManager();
        _lobbyProvider = new UnityLobbyProvider(new UnityLobbyOptions());
        _multiplayerProvider = new RelayMultiplayerProvider();

        _lobbyProvider.JoinToGameSession += JoinGameSession;
    }

    public async void CreateGameSession()
    {
        try
        {
            string joinCode = await _multiplayerProvider.StartGame();
            _lobbyProvider.SetGameJoinCode(joinCode);
            EventSystem.Broadcast(new StartGameSessionEvent());
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw e;
        }
    }

    public async void JoinGameSession(string joinCode)
    {
        try
        {
            await _multiplayerProvider.JoinGame(joinCode);
            EventSystem.Broadcast(new StartGameSessionEvent());
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            throw e;
        }
    }
}
