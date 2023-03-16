using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using SurviveTogether.Data;

namespace SurviveTogether.Network
{
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
                EventSystem.Broadcast(new ConnectEvent());
                string joinCode = await _multiplayerProvider.StartGame();
                _lobbyProvider.SetLevelCustomData(LobbyDataKeys.JOIN_CODE, joinCode);
                EventSystem.Broadcast(new StartGameSessionEvent() { lobby = _lobbyProvider.HostLobby });
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
                EventSystem.Broadcast(new ConnectEvent());
                await _multiplayerProvider.JoinGame(joinCode);
                EventSystem.Broadcast(new StartGameSessionEvent() { lobby = _lobbyProvider.HostLobby});
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw e;
            }
        }

        private void OnApplicationQuit()
        {
            if (_lobbyProvider.IsJoinedToLobby)
            {
                _lobbyProvider.LeaveLobby();
            }
        }
    }
}