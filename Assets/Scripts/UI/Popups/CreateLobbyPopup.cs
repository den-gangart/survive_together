using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateLobbyPopup : Popup
{
    [SerializeField] private TMP_InputField _lobbyNameField;
    [SerializeField] private TMP_Dropdown _playerCountDropDown;
    [SerializeField] private Toggle _isPrivateToggle;
    [Space(10)]
    [SerializeField] private Button _buttonCreate;
    [SerializeField] private Button _buttonClose;

    private const int _selectPlayerCountOffset = 2;

    private void Start()
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

       NetworkSystem.Instance.LobbyProvider.CreateLobby(lobbyParameters);
    }
}
