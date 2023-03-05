using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tween
{
    public class TweenPlayer : MonoBehaviour
    {
        [SerializeField] private string _initialState;
        [SerializeField] private bool _force;

        [Space(10)]
        [SerializeField] private string _currentState;
        private List<ITweenAnimation> _tweenList;

        private void Awake()
        {
            _tweenList = new List<ITweenAnimation>();
            _tweenList.AddRange(GetComponents<ITweenAnimation>());

            if(_tweenList.Count == 0)
            {
                throw new MissingMemberException("Object hasn`t any tween animation");
            }

            if(!string.IsNullOrEmpty(_initialState))
            {
                if (_force) SetFinal(_initialState);
                else Play(_initialState);
            }
        }

        public void Play()
        {
            foreach (var tween in _tweenList)
            {
                tween.Play(_currentState);
            }
        }

        public void Play(string animName)
        {
            _currentState = animName;
            Play();
        }

        public void SetFinal(string animName)
        {
            _currentState = animName;

            foreach (var tween in _tweenList)
            {
                tween.SetFinal(_currentState);
            }
        }
    }
}
