using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.UI
{
    public class UIWindowManager : MonoBehaviour
    {
        [SerializeField] private Popup _base;
        [SerializeField] private Popup _errorMessage;

        private List<Popup> _popupSequence;

        private void Start()
        {
            _popupSequence = new List<Popup>();

            if (_base != null)
            {
                OpenWindow(_base);
            }
        }

        public void OpenWindow(Popup popup)
        {
            if (SequenceContains(popup))
            {
                throw new ArgumentException("Popup has already opened");
            }

            if (_popupSequence.Count != 0)
            {
                _popupSequence[_popupSequence.Count - 1].Close();
            }

            popup.Open();
            popup.PopupClose += CloseWindow;
            _popupSequence.Add(popup);
        }

        public void CloseWindow(Popup popup)
        {
            if (!SequenceContains(popup))
            {
                throw new ArgumentException("Opened popup doesn`t exist");
            }

            popup.Close();
            popup.PopupClose -= CloseWindow;
            _popupSequence.Remove(popup);

            if (_popupSequence.Count != 0)
            {
                _popupSequence[_popupSequence.Count - 1].Open();
            }
        }

        public void ShowErrorMessage()
        {
            _errorMessage.Open();
            _popupSequence.Add(_errorMessage);
        }

        public void ReturnToBase()
        {
            if (_base != null)
            {
                foreach (var popup in _popupSequence)
                {
                    if (popup != _base) CloseWindow(popup);
                }
            }

            _base.Open();
        }

        private bool SequenceContains(Popup popup) => _popupSequence.Contains(popup);
    }
}