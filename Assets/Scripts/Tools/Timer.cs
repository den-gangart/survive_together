using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    public event Action OnTimerDone;
    private float _totalTime;
    private float _leftTime = 0f;
    private bool _autoReset;

    public Timer(float time, bool autoReset = true)
    {
        _autoReset = autoReset;
        Reset(time);
    }

    public void Reset(float newTime)
    {
        _totalTime = newTime;
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

            if(_autoReset)
            {
                Reset(_totalTime);
            }
        }
    }
}
