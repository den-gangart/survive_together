using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween
{
    public class BehaviourTweenAnimation<T1> : MonoBehaviour, ITweenAnimation
    {
        [SerializeField] private List<TwinAnimationParams<T1>> _animationList;

        private bool _isPlaying;
        private float _startTime;
        private float _currentTime;

        protected TwinAnimationParams<T1> _currentAnimation;
        protected T1 _currentValue;

        public void Play(string animationName)
        {
            _currentAnimation = GetAnimation(animationName);

            if (_currentAnimation != null)
            {
                StartPlaying();
            }
        }

        public void SetFinal(string animationName)
        {
            _currentAnimation = GetAnimation(animationName);

            if (_currentAnimation != null)
            {
                _currentValue = _currentAnimation.endParam;
                Apply();
            }
        }

        public void StartPlaying()
        {
            _currentValue = _currentAnimation.startParam;
            _startTime = Time.time;
            _isPlaying = true;
        }

        private void Update()
        {
            if (_isPlaying)
            {
                _currentTime = Time.time - _startTime;
                float time = _currentAnimation.curve.Evaluate(_currentTime / _currentAnimation.duration);

                _currentValue = Lerp(time);

                if (_currentTime >= _currentAnimation.duration)
                {
                    _isPlaying = false;
                    _currentValue = _currentAnimation.endParam;
                }

                Apply();
            }
        }

        private TwinAnimationParams<T1> GetAnimation(string animationName)
        {
            foreach (var current in _animationList)
            {
                if (current.name.Equals(animationName))
                {
                    return current;
                }
            }

            return null;
        }

        protected virtual T1 Lerp(float t) => default(T1);
        protected virtual void Apply() { }
    }
}