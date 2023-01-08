using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public event Action OnTimerDone;
    private float _leftTime = 0f;

    public Timer(float time)
    {
        Reset(time);
    }

    public void Reset(float newTime)
    {
        _leftTime = newTime;
    }

    public void Tick(float deltaTime)
    {
        _leftTime -= deltaTime;
        CheckTimerFinish();
    }

    private void CheckTimerFinish()
    {
        if(_leftTime < 0)
        {
            _leftTime = 0;
            OnTimerDone?.Invoke();
        }
    }
}
