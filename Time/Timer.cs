using System;

namespace Zen.Util
{
    public class Timer
    {
        public event Action OnFinish;
        readonly float _interval;
        readonly bool _isRepeating;
        float _elapsedTime;
        private bool _isDestroyed;

        public Timer(float interval, bool isRepeating, Action callBack)
        {
            _interval = interval;
            _isRepeating = isRepeating;
            OnFinish = callBack;
            System.Console.WriteLine("new timer");
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
