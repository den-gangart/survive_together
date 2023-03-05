using UnityEngine;

namespace Tween
{
    public class Vector3TweenAnimation : BehaviourTweenAnimation<Vector3>
    {
        protected override Vector3 Lerp(float t)
        {
            return Vector3.Lerp(_currentAnimation.startParam, _currentAnimation.endParam, t);
        }
    }

    public class FloatTweenAnimation : BehaviourTweenAnimation<float>
    {
        protected override float Lerp(float t)
        {
            return Mathf.Lerp(_currentAnimation.startParam, _currentAnimation.endParam, t);
        }
    }

    public class ColorTweenAnimation : BehaviourTweenAnimation<Color>
    {
        [SerializeField] private Gradient _gradient;

        protected override Color Lerp(float t)
        {
            return _gradient.Evaluate(t);
        }
    }
}