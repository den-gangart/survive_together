using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;
using System;

public class LobbyPopup : Popup
{
    [SerializeField] private TextMeshProUGUI _lobbyNameText;
    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private TextMeshProUGUI _lobbyCodeText;
    [SerializeField] private Transform _playerLayoutTransform;
    [SerializeField] private PlayerLobbyComponent _playerComponentPrefab;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    private List<PlayerLobbyComponent> _childList = new List<PlayerLobbyComponent>();

    public void InitializeLobby(Lobby lobby)
    {
        SetLobbyCompontents(lobby);
    }

    public void OnLobbyUpdate(Lobby lobby)
    {
        SetLobbyCompontents(lobby);
    }

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartGamePressed);
        _quitButton.onClick.AddListener(OnQuitLobbyPressed);
        LobbyManager.Instance.LobbyUpdate += OnLobbyUpdate;
    }

    private void SetLobbyCompontents(Lobby lobby)
    {
        UpdateTopPanelText(lobby);
        SpawnPlayerList(lobby.Players, lobby.HostId);
        _startButton.gameObject.SetActive(LobbyManager.Instance.IsOwner);
    }

    private void OnStartGamePressed()
    {
        LobbyManager.Instance.StartGameSession();
    }

    private void OnQuitLobbyPressed()
    {
        LobbyManager.Instance.LeaveLobby();
        ClosePopup();
    }

    private void OnDestroy()
    {
        LobbyManager.Instance.LobbyUpdate -= OnLobbyUpdate;
    }

    private void SpawnPlayerList(List<Player> playerList, string hostId)
    {
        int createdObjectCount = _childList.Count;
        int childIndex = 0;

        foreach (var player in playerList)
        {
            PlayerLobbyComponent playerComponent;
            if (childIndex < createdObjectCount)
            {
                playerComponent = _childList[childIndex];
            }
            else
            {
                playerComponent = Instantiate(_playerComponentPrefab, _playerLayoutTransform);
                _childList.Add(playerComponent);
            }

            playerComponent.InitizalizePlayerInfo(player, player.Id.Equals(hostId));
            childIndex++;
        }

        RemoveRemainListObjects(childIndex, createdObjectCount);
    }

    private void RemoveRemainListObjects(int childIndex, int createdObjectCount)
    {
        for (int i = childIndex; i < createdObjectCount; i++)
        {
            Destroy(_childList[i].gameObject);
            _childList.RemoveAt(i);
        }
    }

    private void UpdateTopPanelText(Lobby lobby)
    {
        _lobbyNameText.text = lobby.Name;
        _playerCountText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        _lobbyCodeText.text = lobby.LobbyCode;
    }
}
