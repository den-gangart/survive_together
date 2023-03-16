using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Services.Lobbies.Models;
using SurviveTogether.Network;
using Unity.Netcode;

namespace SurviveTogether.Data
{
    public class NetworkLevelHandler : IDisposable
    {
        private LevelDataManager _levelData;

        public NetworkLevelHandler(LevelDataManager levelData)
        {
            _levelData = levelData;
            EventSystem.AddEventListener<ConnectEvent>(OnConnect);
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnect;
        }

        public void Dispose()
        {
            EventSystem.RemoveEventListener<ConnectEvent>(OnConnect);
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnect;
        }

        private void OnConnect(ConnectEvent e)
        {
            UnityLobbyProvider lobbyProvider = NetworkSystem.Instance.LobbyProvider;

            if (lobbyProvider.IsOwner)
            {
                ProccessServerConnect(lobbyProvider);
            }
            else
            {
                ProccessClientConnect(lobbyProvider);
            }
        }

        private void ProccessServerConnect(UnityLobbyProvider lobbyProvider)
        {
            if (_levelData.IsNewLevel)
            {
                _levelData.GenerateMap();              
            }

            _levelData.PlayerDatas.InitPlayersData(GetPlayerIds(lobbyProvider.HostLobby.Players));
            _levelData.SaveLevel();

            lobbyProvider.SetLevelCustomData(LobbyDataKeys.LEVEL_INFO, _levelData.GetCurrentLevelInJson());
        }

        private void OnClientConnect(ulong clinetID)
        {
            if(!NetworkSystem.Instance.LobbyProvider.IsOwner || clinetID == 0)
            {
                return;
            }

            string playerID = NetworkSystem.Instance.LobbyProvider.HostLobby.Players[(int)clinetID].Id;

            if (!_levelData.PlayerDatas.isPlayerExists(playerID))
            {
                _levelData.PlayerDatas.AddPlayer(playerID);
                _levelData.SaveLevel();
            }
        }

        private void ProccessClientConnect(UnityLobbyProvider lobbyProvider)
        {
           string jsonLevel = lobbyProvider.GetLevelCustomData(LobbyDataKeys.LEVEL_INFO);
            _levelData.ParseLevelFromJson(jsonLevel);
        }

        private List<string> GetPlayerIds(List<Player> players)
        {
            List<string> playerIds = new List<string>();
            foreach (var player in players)
            {
                playerIds.Add(player.Id);
            }
            return playerIds;
        }
    }
}