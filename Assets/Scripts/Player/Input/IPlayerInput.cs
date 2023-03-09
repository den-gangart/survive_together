using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.Players.InputHandle
{
    public interface IPlayerInput
    {
        Vector2 GetMovementInput();
        bool GetAttackInput();
    }
}