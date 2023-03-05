using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tween
{
    public class TransformTween : Vector3TweenAnimation
    {
        protected override void Apply()
        {
            transform.localPosition = _currentValue;
        }
    }
}
