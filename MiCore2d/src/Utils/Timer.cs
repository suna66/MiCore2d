#nullable disable warnings
using System;

namespace MiCore2d
{
    public class Timer
    {
        protected class TimerInfo
        {
            public int id;
            public double time;
            public Action func;
            public bool isActive;

            public TimerInfo(int timerId, double msec, Action callback)
            {
                id = timerId;
                time = msec / 1000.0f;
                func = callback;
                isActive = true;
            }
        };

        private int _id = 0;
        private List<TimerInfo>? _timerList = null;
        public Timer()
        {
            _timerList = new List<TimerInfo>();
        }

        public void Update(double elapsed)
        {
            foreach(TimerInfo info in _timerList)
            {
                if (info.isActive)
                {
                    info.time -= elapsed;
                    if (info.time < 0.0f)
                    {
                        info.func();
                        info.isActive = false;
                    }
                }
            }
            _timerList.RemoveAll(p => p.isActive == false);
        }

        public int AddTimer(double msec, Action func)
        {
            TimerInfo info = new TimerInfo(_id++, msec, func);
            _timerList.Add(info);

            return info.id;
        }

        public void KillTimer(int timerId)
        {
            foreach(TimerInfo info in _timerList)
            {
                if (info.id == timerId)
                {
                    info.isActive = false;
                }
            }
        }

        public void Dispose()
        {
            _timerList = null;
        }
    }
}