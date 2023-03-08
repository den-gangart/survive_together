using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMobileInput : IPlayerInput
{
    ScreenInput _screenInput;
    private Joystick _joystick;
    public void Setup(ScreenInput screenInput)
    {
        _screenInput = screenInput;
        _joystick = _screenInput.Joystick; 
    }

    public bool GetAttackInput()
    {
        throw new System.NotImplementedException();
    }

    public Vector2 GetMovementInput()
    {
        if (_joystick != null)
        {
            return new Vector2(_joystick.Horizontal, _joystick.Vertical);
        }

        return Vector2.zero;
    }
}
