using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameManager : Singleton<PlayerNameManager>
{
    [SerializeField] private string _defaultName;
    private PlayerNameData _playerNameData;
    public string PlayerName { get { return _playerNameData.PlayerName; } }

    protected override void OnAwake()
    {
        _playerNameData = new PlayerNameData(_defaultName);
        EventSystem.AddEventListener<NameChangeEvent>(_playerNameData.ChangeName);
    }

    private void OnDestroy()
    {
        EventSystem.RemoveEventListener<NameChangeEvent>(_playerNameData.ChangeName);
    }
}
