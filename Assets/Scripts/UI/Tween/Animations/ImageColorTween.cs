using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    [RequireComponent(typeof(Image))]
    public class ImageColorTween : ColorTweenAnimation
    {
        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
        }

        protected override void Apply()
        {
            _image.color = _currentValue;
        }
    }
}