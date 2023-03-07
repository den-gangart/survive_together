using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurviveTogether.Player
{
    [RequireComponent(typeof(Animator))]
    public class AnimationPlayerView : MonoBehaviour, IPlayerView
    {
        private Animator _animator;
        private const string X_VELOCITY_KEY = "XVelocity";
        private const string Y_VELOCITY_KEY = "YVelocity";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Move(Vector2 velocity)
        {
            _animator.SetFloat(X_VELOCITY_KEY, velocity.x);
            _animator.SetFloat(Y_VELOCITY_KEY, velocity.y);
        }
    }
}

