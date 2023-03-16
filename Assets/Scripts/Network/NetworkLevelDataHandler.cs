using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Services.Lobbies.Models;
using SurviveTogether.Network;
using SurviveTogether.World;
using Unity.Netcode;
using Zenject;

namespace SurviveTogether.Data
{
    public class NetworkLevelDataHandler : NetworkBehaviour
    {
        [Inject] private LevelDataManager _levelData;
        [SerializeField] LevelLoader _levelLoader;
        private string _jsonLevelData;
        private const int STRING_BUFFER_SIZE = 500;
       
        private void Start()
        {
            UnityLobbyProvider lobbyProvider = NetworkSystem.Instance.LobbyProvider;

            if (lobbyProvider.IsOwner)
            {
                ProccessServerConnect(lobbyProvider);
                _levelLoader.LoadMap();
            }
            else
            {
                _jsonLevelData = "";
            }
        }
        private void OnEnable()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnect;
        }

        private void OnDisable()
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnect;
        }

        private void ProccessServerConnect(UnityLobbyProvider lobbyProvider)
        {
            if (_levelData.IsNewLevel)
            {
                _levelData.GenerateMap();              
            }

            _levelData.PlayerDatas.InitPlayersData(GetPlayerIds(lobbyProvider.HostLobby.Players));
            _levelData.SaveLevel();
        }

        [ClientRpc]
        private void SendLevelDataClientRpc(string levelData, ClientRpcParams clientRpcParams = default)
        {
            if (IsOwner) return;

            _jsonLevelData += levelData;
        }

        [ClientRpc]
        private void ConvertLevelInfoClientRpc()
        {
            if (IsOwner) return;

            _levelData.ParseLevelFromJson(_jsonLevelData);
            _levelLoader.LoadMap();
        }
        
        private void OnClientConnect(ulong clientID)
        {
            if(!NetworkSystem.Instance.LobbyProvider.IsOwner || clientID == 0)
            {
                return;
            }

            string playerID = NetworkSystem.Instance.LobbyProvider.HostLobby.Players[(int)clientID].Id;

            if (!_levelData.PlayerDatas.isPlayerExists(playerID))
            {
                _levelData.PlayerDatas.AddPlayer(playerID);
                _levelData.SaveLevel();
            }

            ProccessClientConnect(clientID);
        }

        private void ProccessClientConnect(ulong clientID)
        {
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new ulong[] { clientID }
                }
            };

            StartCoroutine(SendParticulalLevelData(clientRpcParams));
        }

        private IEnumerator SendParticulalLevelData(ClientRpcParams clientRpcParams)
        {
            string jsonLevelData = _levelData.GetCurrentLevelInJson();

            for (int i = 0; i < jsonLevelData.Length; i += STRING_BUFFER_SIZE)
            {
                if (i + STRING_BUFFER_SIZE < jsonLevelData.Length)
                {
                    SendLevelDataClientRpc(jsonLevelData.Substring(i, STRING_BUFFER_SIZE), clientRpcParams);
                }
                else
                {
                    SendLevelDataClientRpc(jsonLevelData.Substring(i), clientRpcParams);
                }

                yield return null;
            }

            ConvertLevelInfoClientRpc();
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