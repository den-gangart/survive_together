using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameManager : Singleton<PlayerNameManager>
{
    private PlayerNameData _playerNameData;
    public string PlayerName { get { return _playerNameData.PlayerName; } }

    protected override void OnAwake()
    {
        _playerNameData = new PlayerNameData();
        EventSystem.AddEventListener<NameChangeEvent>(OnNameChanged);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener<NameChangeEvent>(OnNameChanged);
    }

    private void OnNameChanged(NameChangeEvent nameChangeEvent)
    {
        _playerNameData.ChangeName(nameChangeEvent.name);
    }
}
