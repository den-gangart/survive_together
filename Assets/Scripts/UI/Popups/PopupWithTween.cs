using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tween;

namespace SurviveTogether.UI
{
    [RequireComponent(typeof(TweenPlayer))]
    public class PopupWithTween : Popup
    {
        private TweenPlayer _tweenPlayer;
        private const string SHOW_ANIMATION = "Show";
        private const string HIDE_ANIMATION = "Hide";

        private void Start()
        {
            _tweenPlayer = GetComponent<TweenPlayer>();
            OnStart();
        }

        public override void Open()
        {
            _tweenPlayer.Play(SHOW_ANIMATION);
            base.Open();
        }

        public override void Close()
        {
            _tweenPlayer.Play(HIDE_ANIMATION);
            base.Close();
        }
    }
}