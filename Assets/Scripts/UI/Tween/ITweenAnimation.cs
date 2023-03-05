using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tween
{
    public interface ITweenAnimation
    {
        void Play(string animName);
        void SetFinal(string animName);
    }
}