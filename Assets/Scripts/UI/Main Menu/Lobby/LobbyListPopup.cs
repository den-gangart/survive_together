using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SurviveTogether.UI
{
    public class LobbyListPopup : PopupWithTween
    {
        [Header(EditorTitles.FIELDS_HEADER)]
        [SerializeField] private Transform _lobbyLayoutTransform;
        [SerializeField] private JoinLobbyComponent _lobbyComponentPrefab;
        [SerializeField] private TMP_InputField _joinCodeField;

        [Header(EditorTitles.BUTTONS_HEADER)]
        [SerializeField] private Button _refreshButton;
        [SerializeField] private Button _joinByIdButton;
        [SerializeField] private Button _quitButton;

        [Inject] private LobbyMenuHandler _lobbyHandler;

        protected override void OnStart()
        {
            _refreshButton.onClick.AddListener(OnRefresh);
            _joinByIdButton.onClick.AddListener(OnJoinByCode);
            _quitButton.onClick.AddListener(OnQuitLobbyPressed);
        }

        public override void Open()
        {
            OnRefresh();
            base.Open();
        }

        public override void Close()
        {
            ClearLobbyList();
            base.Close();
        }

        private void OnLobbyListLoaded(List<Lobby> lobbies)
        {
            ClearLobbyList();
            SpawnLobbyList(lobbies);
        }

        private void OnJoinById(string id)
        {
            _lobbyHandler.JoinLobbyWithId(id);
        }

        private void OnJoinByCode()
        {
            string code = _joinCodeField.text;
            _lobbyHandler.JoinLobbyWithCode(code);
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
            _lobbyHandler.LoadLobbyList(OnLobbyListLoaded);
        }

        private void OnQuitLobbyPressed()
        {
            ClosePopup();
        }
    }
}