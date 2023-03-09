using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.Players.InputHandle
{
    public class PlayerPCInput : IPlayerInput
    {
        public bool GetAttackInput()
        {
            throw new System.NotImplementedException();
        }

        public Vector2 GetMovementInput()
        {
            float horizontalInput = Input.GetAxis(AxisInputKeys.HORIZONTAL);
            float verticalInput = Input.GetAxis(AxisInputKeys.VERTICAL);
            return new Vector2(horizontalInput, verticalInput);
        }
    }
}