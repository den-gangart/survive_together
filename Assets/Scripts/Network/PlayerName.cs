using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameData
{
    private string _playerName;
    public string PlayerName { get { return _playerName; } }

    public PlayerNameData(string defaultName)
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.PLAYER_NAME))
        {
            _playerName = PlayerPrefs.GetString(PlayerPrefsKeys.PLAYER_NAME, defaultName);
        }
        else
        {
            PlayerPrefs.SetString(PlayerPrefsKeys.PLAYER_NAME, defaultName);
            _playerName = defaultName;
        }
    }

    public void ChangeName(NameChangeEvent nameChangeEvent)
    {
        _playerName = nameChangeEvent.name;
        PlayerPrefs.SetString(PlayerPrefsKeys.PLAYER_NAME, _playerName);
        Debug.Log("Player name changed to: " + _playerName);
    }
}
