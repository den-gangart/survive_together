using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ScreenInput: MonoBehaviour
{
    public Joystick Joystick { get { return _fixedJoystick; } }
    [SerializeField] private Joystick _fixedJoystick;

    [Inject] private InputType _inputType;
    [Inject] private IPlayerInput _playerInput;

    private void Awake()
    {
        if(_inputType == InputType.Devices)
        {
            Destroy(gameObject);
        }
        else
        {
           ((PlayerMobileInput)_playerInput).Setup(this);
        }
    }
}