using System.Collections.Generic;

namespace Zen.Util
{
    public class TimerManager
    {
        private readonly List<Timer> _toRemove = new List<Timer>();
        private readonly List<Timer> _timers = new List<Timer>();
        public static TimerManager Instance;

        public TimerManager()
        {
            Instance = this;
        }

        public static void Add(Timer timer) { Instance._timers.Add(timer); }
        public static void Remove(Timer timer) { Instance._timers.Remove(timer); }

        public void Update()
        {
            for (var i = _timers.Count - 1; i >= 0; i--)
                _timers[i].Update();
        }
    }
}

