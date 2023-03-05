using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;
using System;
using Zenject;

public class LobbyPopup : PopupWithTween
{
    [Header(EditorTitles.FIELDS_HEADER)]
    [SerializeField] private TextMeshProUGUI _lobbyNameText;
    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private TextMeshProUGUI _lobbyCodeText;

    [Header(EditorTitles.BUTTONS_HEADER)]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    [Space(10)]
    [SerializeField] private Transform _playerLayoutTransform;
    [SerializeField] private PlayerLobbyComponent _playerComponentPrefab;
   
    private List<PlayerLobbyComponent> _childList = new List<PlayerLobbyComponent>();
    [Inject] private LobbyMenuHandler _lobbyHandler;

    public void InitializeLobby(Lobby lobby)
    {
        SetLobbyCompontents(lobby);
    }

    public void OnLobbyUpdate(Lobby lobby)
    {
        SetLobbyCompontents(lobby);
    }

    public override void Open()
    {
       _lobbyHandler.LobbyUpdate += OnLobbyUpdate;
        base.Open();
    }

    public override void Close()
    {
        _lobbyHandler.LobbyUpdate -= OnLobbyUpdate;
        base.Close();
    }

    protected override void OnStart()
    {
        _startButton.onClick.AddListener(OnStartGamePressed);
        _quitButton.onClick.AddListener(OnQuitLobbyPressed);
    }

    private void SetLobbyCompontents(Lobby lobby)
    {
        UpdateTopPanelText(lobby);
        SpawnPlayerList(lobby.Players, lobby.HostId);
        _startButton.gameObject.SetActive(_lobbyHandler.IsLobbyOwner());
    }

    private void OnStartGamePressed()
    {
        _lobbyHandler.CreateGameSession();
    }

    private void OnQuitLobbyPressed()
    {
        _lobbyHandler.LeaveLobby();
        ClosePopup();
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
