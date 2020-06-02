/*
    Studiengang MultimediaTechnology, FH Salzburg
    Multimediaprojekt 1
    Author: Samuel Femböck
*/

using System;
using Zen;

public class Timer
{
    public event Action onFinish;
    float _interval;
    float _elapsed;
    bool _destroyed;
    bool _isRepeating;

    public Timer(float interval, bool isRepeating, Action callBack)
    {
        this._interval = interval;
        onFinish = callBack;
        _isRepeating = isRepeating;
        TimerManager.Add(this);
    }

    public void Update()
    {
        if (_destroyed) return;
        _elapsed += Time.DeltaTime;

        if (_elapsed >= _interval)
        {
            onFinish?.Invoke();
            
            if (_isRepeating)
                _elapsed = 0;
            else
                Destroy();
        }
    }

    public void Destroy()
    {
        _destroyed = true;
        TimerManager.Remove(this);
    }
}
