using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.Players
{
   // [RequireComponent(typeof(Animator))]
    public class AnimationPlayerView : MonoBehaviour, IPlayerView
    {
        [SerializeField]
        private Animator _animator;
        private const string X_VELOCITY_KEY = "XVelocity";
        private const string Y_VELOCITY_KEY = "YVelocity";


        public void Move(Vector2 velocity)
        {
            _animator.SetFloat(X_VELOCITY_KEY, velocity.x);
            _animator.SetFloat(Y_VELOCITY_KEY, velocity.y);
        }
    }
}

