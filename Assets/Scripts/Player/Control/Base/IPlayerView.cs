using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SurviveTogether.Player
{
    public interface IPlayerView
    {
        void Move(Vector2 velocity);
    }
}