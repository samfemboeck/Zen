/*
    Studiengang MultimediaTechnology, FH Salzburg
    Multimediaprojekt 1
    Author: Samuel Femböck
*/

using System;
using Zen;
using Zen.Util;

namespace Zen.Util
{
    public class Timer
    {
        public event Action OnFinish;
        private readonly float _interval;
        private readonly bool _isRepeating;
        private float _elapsedTime;
        private bool _isDestroyed;

        public Timer(float interval, bool isRepeating, Action callBack)
        {
            _interval = interval;
            OnFinish = callBack;
            _isRepeating = isRepeating;
            TimerManager.Add(this);
        }

        public void Update()
        {
            if (_isDestroyed) return;
            _elapsedTime += Time.DeltaTime;

            if (!(_elapsedTime >= _interval)) return;
            
            OnFinish?.Invoke();
            
            if (_isRepeating)
                _elapsedTime = 0;
            else
                Destroy();
        }

        public void Destroy()
        {
            _isDestroyed = true;
            TimerManager.Remove(this);
        }
    }
}
