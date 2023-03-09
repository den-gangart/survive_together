using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.Players
{
    public interface IPlayerMovement
    {
        void Move(Vector2 velocity);
    }
}