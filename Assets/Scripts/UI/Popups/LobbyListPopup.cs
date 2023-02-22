using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyListPopup : Popup
{
    [SerializeField] private Transform _lobbyLayoutTransform;
    [SerializeField] private JoinLobbyComponent _lobbyComponentPrefab;
    [SerializeField] private TMP_InputField _joinCodeField;
    [SerializeField] private Button _refreshButton;
    [SerializeField] private Button _joinByIdButton;
    [SerializeField] private Button _quitButton;

    private void Start()
    {
        _refreshButton.onClick.AddListener(OnRefresh);
        _joinByIdButton.onClick.AddListener(OnJoinByCode);
        _quitButton.onClick.AddListener(OnQuitLobbyPressed);
        OnRefresh();
    }

    private void OnLobbyListLoaded(List<Lobby> lobbies)
    {
        ClearLobbyList();
        SpawnLobbyList(lobbies);
    }

    private void OnJoinById(string id)
    {
        NetworkSystem.Instance.LobbyProvider.JoinLobbyById(id);
    }

    private void OnJoinByCode()
    {
        string code = _joinCodeField.text;
        NetworkSystem.Instance.LobbyProvider.JoinLobbyByCode(code);
    }

    private void OnQuitLobbyPressed()
    {
        ClosePopup();
    }

    private void ClearLobbyList()
    {
        for (int i = 0; i < _lobbyLayoutTransform.childCount; i++)
        {
            Destroy(_lobbyLayoutTransform.GetChild(i).gameObject);
        }
    }

    private void SpawnLobbyList(List<Lobby> lobbyList)
    {
        foreach (var lobby in lobbyList)
        {
            var lobbyComponent = Instantiate(_lobbyComponentPrefab, _lobbyLayoutTransform);
            lobbyComponent.InitizalizeLobbyInfo(lobby, OnJoinById);
        }
    }

    private void OnRefresh()
    {
        NetworkSystem.Instance.LobbyProvider.LoadLobbyList(OnLobbyListLoaded);
    }
}
