using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace SurviveTogether.UI
{
    public class MainMenuController : UIWindowManager
    {
        [Header(EditorTitles.BUTTONS_HEADER)]
        [SerializeField] private Button _joinLobbyButton;
        [SerializeField] private Button _createLobbyButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _exitButton;

        [Header(EditorTitles.POPUPS_HEADER)]
        [SerializeField] private Popup _joinLobbyWindow;
        [SerializeField] private Popup _createLobbyWindow;
        [SerializeField] private Popup _lobbyWindow;
        [SerializeField] private Popup _connectionWindow;

        private void Awake()
        {
            _joinLobbyButton.onClick.AddListener(OnJoinLobbyPressed);
            _createLobbyButton.onClick.AddListener(OnCreateLobbyPressed);
            _settingsButton.onClick.AddListener(OnSettingsPressed);
            _soundButton.onClick.AddListener(OnSoundPressed);
            _exitButton.onClick.AddListener(OnExitPressed);
        }

        private void OnJoinLobbyPressed() => OpenWindow(_joinLobbyWindow);
        private void OnCreateLobbyPressed() => OpenWindow(_createLobbyWindow);
        private void OpenLobby() => OpenWindow(_lobbyWindow);
        private void OnSettingsPressed() => throw new NotImplementedException();
        private void OnSoundPressed() => throw new NotImplementedException();
        private void OnExitPressed() => Application.Quit();

        private void OnCreateLobbyEvent(CreateLobbyEvent e) => OpenLobby();
        private void OnJoinLobbyEvent(JoinLobbyEvent e) => OpenLobby();
        private void ConnectEvent(ConnectEvent e) => OpenWindow(_connectionWindow);

        private void OnEnable()
        {
            EventSystem.AddEventListener<CreateLobbyEvent>(OnCreateLobbyEvent);
            EventSystem.AddEventListener<JoinLobbyEvent>(OnJoinLobbyEvent);
            EventSystem.AddEventListener<ConnectEvent>(ConnectEvent);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener<CreateLobbyEvent>(OnCreateLobbyEvent);
            EventSystem.RemoveEventListener<JoinLobbyEvent>(OnJoinLobbyEvent);
            EventSystem.RemoveEventListener<ConnectEvent>(ConnectEvent);
        }
    }
}