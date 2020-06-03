using System.Collections.Generic;

namespace Zen.Util
{
    public class TimerManager
    {
        readonly List<Timer> _toRemove = new List<Timer>();
        readonly List<Timer> _timers = new List<Timer>();
        static TimerManager _instance;

        public static TimerManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimerManager();

                return _instance;
            }
            private set => _instance = value;
        }

        TimerManager() {}

        public static void Add(Timer timer) { Instance._timers.Add(timer); }
        public static void Remove(Timer timer) { Instance._timers.Remove(timer); }

        public void Update()
        {
            for (var i = _timers.Count - 1; i >= 0; i--)
                _timers[i].Update();
        }
    }
}

