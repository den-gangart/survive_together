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
    [SerializeField] private Transform _playerLayoutTransform;
    [SerializeField] private PlayerLobbyComponent _playerComponentPrefab;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _startButton.onClick.AddListener(OnStartGamePressed);
        _quitButton.onClick.AddListener(OnQuitLobbyPressed);
        LobbyManager.Instance.Provider.LobbyUpdate += OnLobbyUpdate;
    }

    private void OnDestroy()
    {
        LobbyManager.Instance.Provider.LobbyUpdate -= OnLobbyUpdate;
    }

    public void InitializeLobby(Lobby lobby)
    {
        SetLobbyCompontents(lobby);
    }

    public void OnLobbyUpdate(Lobby lobby)
    {
        SetLobbyCompontents(lobby);
    }

    private void SetLobbyCompontents(Lobby lobby)
    {
        _lobbyNameText.text = lobby.Name;
        _playerCountText.text = lobby.Players.Count + "/" + lobby.MaxPlayers;
        ClearPlayerList();
        SpawnPlayerList(lobby.Players);

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
        ClosePopup();
    }

    private void ClearPlayerList()
    {
        for (int i = 0; i < _playerLayoutTransform.childCount; i++)
        {
            Destroy(_playerLayoutTransform.GetChild(i).gameObject);
        }
    }

    private void SpawnPlayerList(List<Player> playerList)
    {
        foreach(var player in playerList)
        {
            var playerComponent = Instantiate(_playerComponentPrefab, _playerLayoutTransform);
            playerComponent.InitizalizePlayerInfo(player);
        }
    }
}
