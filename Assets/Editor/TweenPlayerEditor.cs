using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tween
{
    [CustomEditor(typeof(TweenPlayer), true)]
    public class TweenAnimationEditor : Editor
    {
        private const string EDITOR_AREA = "EDITOR";
        private const string BUTTON_PLAY_TEXT = "Play";
        private const int EDITOR_LINE_SPACE = 10;

        private string _animName;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(EDITOR_LINE_SPACE);
            GUILayout.Label(EDITOR_AREA);

            _animName = GUILayout.TextField(_animName);

            if (GUILayout.Button(BUTTON_PLAY_TEXT))
            {
                TweenPlayer tweenPlayer = (TweenPlayer)target;
                tweenPlayer.Play(_animName);
            }
        }
    }
}
