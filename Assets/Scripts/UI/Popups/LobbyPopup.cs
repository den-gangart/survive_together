using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine.UI;

public class LobbyPopup : Popup
{
    [SerializeField] private TextMeshProUGUI _lobbyNameText;
    [SerializeField] private TextMeshProUGUI _playerCountText;
    [SerializeField] private TextMeshProUGUI _lobbyCodeText;
    [SerializeField] private Transform _playerLayoutTransform;
    [SerializeField] private PlayerLobbyComponent _playerComponentPrefab;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

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

    private void OnDestroy()
    {
        LobbyManager.Instance.LobbyUpdate -= OnLobbyUpdate;
    }

    private void SetLobbyCompontents(Lobby lobby)
    {
        _lobbyNameText.text = lobby.Name;
        _playerCountText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        _lobbyCodeText.text = lobby.LobbyCode;

        ClearPlayerList();
        SpawnPlayerList(lobby.Players, lobby.HostId);

        if(LobbyManager.Instance.IsOwner)
        {
            _startButton.gameObject.SetActive(true);
        }
        else
        {
            _startButton.gameObject.SetActive(false);
        }
    }

    private void OnStartGamePressed()
    {

    }

    private void OnQuitLobbyPressed()
    {
        LobbyManager.Instance.LeaveLobby();
        ClosePopup();
    }

    private void ClearPlayerList()
    {
        for (int i = 0; i < _playerLayoutTransform.childCount; i++)
        {
            Destroy(_playerLayoutTransform.GetChild(i).gameObject);
        }
    }

    private void SpawnPlayerList(List<Player> playerList, string hostId)
    {
        foreach(var player in playerList)
        {
            var playerComponent = Instantiate(_playerComponentPrefab, _playerLayoutTransform);
            bool isOwner = player.Id.Equals(hostId);
            playerComponent.InitizalizePlayerInfo(player, isOwner);
        }
    }
}
