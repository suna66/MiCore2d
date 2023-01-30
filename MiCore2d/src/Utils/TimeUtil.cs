#nullable disable warnings
#pragma warning disable CS1998
using System;
using System.Threading.Tasks;

namespace MiCore2d
{
    public class TimeUtil
    {

        public static void Delay(int msec, Action func)
        {
            Task.Run(async() => executeTask(msec, func) );
        }

        private static　async void executeTask(int msec, Action func)
        {
            await Task.Delay(msec);
            func();
        }
    }
}