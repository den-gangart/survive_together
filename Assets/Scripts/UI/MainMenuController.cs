using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Transform _menuTransform;
    [SerializeField] private Transform _popupTransform;

    [Space(10)]
    [SerializeField] private Button _joinLobbyButton;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _quitButton;

    [Space(10)]
    [SerializeField] private PopupData _popupData;
    private Popup _activePopup;

    private void Awake()
    {
        _joinLobbyButton.onClick.AddListener(OnJoinPressed);
        _createLobbyButton.onClick.AddListener(OnCreatePressed);
        _quitButton.onClick.AddListener(OnQuitPressed);
        EventSystem.AddEventListener<CreateLobbyEvent>(OnLobbyCreated);
        EventSystem.AddEventListener<JoinLobbyEvent>(OnJoinedToLobby);
    }

    private void OnJoinPressed()
    {
        CreatePopup(_popupData.joinLobby);
    }

    private void OnCreatePressed()
    {
        CreatePopup(_popupData.createLobby);
    }

    private void OnPopupClosed(Popup popup)
    {
        Destroy(popup.gameObject);
        _menuTransform.gameObject.SetActive(true);
    }

    private void CreatePopup(Popup popup)
    {
        Popup createdPopup = Instantiate(popup, _popupTransform);
        createdPopup.PopupClosed += OnPopupClosed;
        _menuTransform.gameObject.SetActive(false);
        _activePopup = createdPopup;
    }

    private void OnLobbyCreated(CreateLobbyEvent createLobbyEvent)
    {
        OpenLobby(createLobbyEvent.lobby);
    }

    private void OnJoinedToLobby(JoinLobbyEvent joinLobbyEvent)
    {
        OpenLobby(joinLobbyEvent.lobby);
    }

    private void OpenLobby(Lobby lobby)
    {
        if (_activePopup != null)
        {
            Destroy(_activePopup.gameObject);
        }

        CreatePopup(_popupData.lobby);
    }

    private void OnQuitPressed()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener<CreateLobbyEvent>(OnLobbyCreated);
        EventSystem.RemoveEventListener<JoinLobbyEvent>(OnJoinedToLobby);
    }
}
