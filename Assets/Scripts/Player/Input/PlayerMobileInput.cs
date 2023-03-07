using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMobileInput : IPlayerInput
{
    [Inject] FixedJoystick _fixedJoystick;

    public bool GetAttackInput()
    {
        throw new System.NotImplementedException();
    }

    public Vector2 GetMovementInput()
    {
        return new Vector2(_fixedJoystick.Horizontal, _fixedJoystick.Vertical);
    }
}
