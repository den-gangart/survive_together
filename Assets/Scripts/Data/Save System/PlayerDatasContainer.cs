using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SurviveTogether.Data
{
    [System.Serializable]
    public class PlayerDataContainer
    {
        public List<PlayerData> _playerDatasList;

        public PlayerDataContainer()
        {
            _playerDatasList = new List<PlayerData>();
        }

        public PlayerDataContainer(List<PlayerData> playerDataList)
        {
            _playerDatasList = playerDataList;
        }

        public void InitPlayersData(List<string> playerIds)
        {
            foreach (var playerId in playerIds)
            {
                if (isPlayerExists(playerId) == false)
                {
                    AddPlayer(playerId);
                }
            }
        }

        public void UpdatePlayer(PlayerData playerData)
        {
            if(isPlayerExists(playerData.id))
            {
                _playerDatasList[GetPlayerIndex(playerData.id)] = playerData;
            }
            else
            {
                AddPlayer(playerData.id);
            }
        }

        public void AddPlayer(string playerId)
        {
            _playerDatasList.Add(PlayerData.GenerateInitialPlayerData(playerId));
        }

        public void AddPlayer(PlayerData playerData)
        {
            _playerDatasList.Add(playerData);
        }

        public bool isPlayerExists(string playerId)
        {
            return GetPlayerIndex(playerId) >= 0;
        }

        private int GetPlayerIndex(string playerId)
        {
            return _playerDatasList.FindIndex((playerData) => playerData.id.Equals(playerId));
        }
    }
}