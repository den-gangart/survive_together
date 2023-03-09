using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using SurviveTogether.Data;

namespace SurviveTogether.UI
{
    public class CreateLobbyPopup : PopupWithTween
    {
        [Header(EditorTitles.FIELDS_HEADER)]
        [SerializeField] private TMP_InputField _lobbyNameField;
        [SerializeField] private TMP_Dropdown _playerCountDropDown;
        [SerializeField] private Toggle _isPrivateToggle;

        [Header(EditorTitles.BUTTONS_HEADER)]
        [SerializeField] private Button _buttonCreate;
        [SerializeField] private Button _buttonClose;

        private const int _selectPlayerCountOffset = 2;
        [Inject] private LobbyMenuHandler _lobbyHandler;

        protected override void OnStart()
        {
            _buttonCreate.onClick.AddListener(CreateLobby);
            _buttonClose.onClick.AddListener(ClosePopup);
        }

        private void CreateLobby()
        {
            LobbyParameters lobbyParameters = new LobbyParameters(
                _lobbyNameField.text,
                _playerCountDropDown.value + _selectPlayerCountOffset,
                _isPrivateToggle.isOn
            );

            _lobbyHandler.CreateLobby(lobbyParameters);
        }
    }
}