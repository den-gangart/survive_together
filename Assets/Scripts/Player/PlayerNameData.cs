using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameData
{
    private string _defaultName = "Player";
    private string _playerName;
    public string PlayerName { get { return _playerName; } }

    public PlayerNameData()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.PLAYER_NAME))
        {
            _playerName = PlayerPrefs.GetString(PlayerPrefsKeys.PLAYER_NAME, _defaultName);
        }
        else
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.PLAYER_NAME, _defaultName);
            _playerName = _defaultName;
        }
    }

    public void ChangeName(string newName)
    {
        _playerName = newName;
        PlayerPrefs.SetString(PlayerPrefsKeys.PLAYER_NAME, _playerName);
        Debug.Log("Player name changed to: " + _playerName);
    }
}
