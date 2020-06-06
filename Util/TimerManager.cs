using System.Collections.Generic;

namespace Zen.Util
{
    public class TimerManager
    {
        List<Timer> _toRemove = new List<Timer>();
        List<Timer> _toAdd = new List<Timer>();
        List<Timer> _timers = new List<Timer>();

        static TimerManager _instance = null;

        public static TimerManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TimerManager();

                return _instance;
            }
        }

        TimerManager() {}

        public static void Add(Timer timer) => Instance._toAdd.Add(timer); 
        public static void Remove(Timer timer) => Instance._toRemove.Add(timer);

        public void Update()
        {
            foreach (Timer timer in _toRemove) _timers.Remove(timer);
            _toRemove.Clear();
            
            foreach (Timer timer in _toAdd) _timers.Add(timer);
            _toAdd.Clear();
            
            foreach (Timer timer in _timers) timer.Update();
        }
    }
}

