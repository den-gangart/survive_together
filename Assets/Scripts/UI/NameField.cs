using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameField : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    private string _currentName;

    private void Start()
    {
        _currentName = PlayerNameManager.Instance.PlayerName;
        _inputField.text = _currentName;
        _inputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string newName)
    {
        if(_currentName != newName)
        {
            EventSystem.Broadcast(new NameChangeEvent { name = newName });
        }
    }
}
