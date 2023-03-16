using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace SurviveTogether.UI
{
    [RequireComponent(typeof(Button))]
    public class MapElementForList : MonoBehaviour, ISelectableItem
    {
        public event Action<MapElementForList> ElementSelected;
        public bool IsSelected { get { return _isSelected; } }
        public string Name { get { return _mapNameInputField.text; } }

        [SerializeField] private GameObject _border;
        [SerializeField] private TextMeshProUGUI _mapNameText;
        [SerializeField] private TextMeshProUGUI _dateCreateText;
        [SerializeField] private TextMeshProUGUI _dateModifyText;
        [SerializeField] private TMP_InputField _mapNameInputField;
        private bool _isSelected;

        private void Awake()
        {
            _isSelected = false;
            GetComponent<Button>().onClick.AddListener(Select);
        }

        public void InitizalizeAsExisted(string name, DateTime creationDate, DateTime modifyDate)
        {
            _mapNameText.text = name;
            _dateCreateText.text = $"Created: {creationDate.ToShortDateString()}";
            _dateModifyText.text = $"Modified: {modifyDate.ToShortDateString()}";
        }

        public void InitizalizeAsNew(string defaultName)
        {
            _mapNameInputField.text = defaultName;
        }

        public void Deselect()
        {
            if (_isSelected)
            {
                UpdateState(false); 
            }
        }

        public void Select()
        {
            if (!_isSelected)
            {
                ElementSelected?.Invoke(this);
                UpdateState(true);
            }
        }

        private void UpdateState(bool newValue)
        {
            _isSelected = newValue;
            _border.SetActive(newValue);
        }
    }
}