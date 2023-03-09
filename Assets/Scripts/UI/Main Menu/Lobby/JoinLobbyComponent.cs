using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Lobbies.Models;
using System;

namespace SurviveTogether.UI
{
    public class JoinLobbyComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _lobbyNameText;
        [SerializeField] private TextMeshProUGUI _playerCountText;
        [SerializeField] private Button _joinButton;

        public void InitizalizeLobbyInfo(Lobby lobby, Action<string> JoinLobby)
        {
            ;
            _lobbyNameText.text = lobby.Name;
            _playerCountText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
            _joinButton.onClick.AddListener(delegate { JoinLobby(lobby.Id); });
        }
    }
}