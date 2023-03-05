using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween
{
    public class ScaleTween : Vector3TweenAnimation
    {
        protected override void Apply()
        {
            transform.localScale = _currentValue;
        }
    }
}