using System.Collections.Generic;

namespace Zen.Util
{
    public static class TimerManager
    {
        static List<Timer> _toRemove = new List<Timer>();
        static List<Timer> _toAdd = new List<Timer>();
        static List<Timer> _timers = new List<Timer>();

        public static void Add(Timer timer) => _toAdd.Add(timer); 
        public static void Remove(Timer timer) => _toRemove.Add(timer);

        public static void Update()
        {
            foreach (Timer timer in _toRemove) _timers.Remove(timer);
            _toRemove.Clear();
            
            foreach (Timer timer in _toAdd) _timers.Add(timer);
            _toAdd.Clear();
            
            foreach (Timer timer in _timers) timer.Update();
        }
    }
}

