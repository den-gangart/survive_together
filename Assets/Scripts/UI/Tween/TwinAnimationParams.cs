using UnityEngine;

namespace Tween
{
    [System.Serializable]
    public class TwinAnimationParams<T>
    {
        public string name;
        public T startParam;
        public T endParam;
        public float duration;
        public AnimationCurve curve;
    }
}

