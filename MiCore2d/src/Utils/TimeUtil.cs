#nullable disable warnings
#pragma warning disable CS1998
using System;
using System.Threading.Tasks;

namespace MiCore2d
{
    /// <summary>
    ///  TimeUtil
    /// </summary>
    /// <remarks>Utility class for timer</remarks>
    public class TimeUtil
    {

        /// <summary>
        /// Delay.
        /// </summary>
        /// <remarks>call specified function after consumed specified seconds.</remarks>
        /// <param name="msec">mili-second</param>
        /// <param naem="func">called function after specified time</param>
        public static void Delay(int msec, Action func)
        {
            Task.Run(async() => executeTask(msec, func) );
        }

        /// <summary>
        /// executeTask. actual functino to delay calling specified function.
        /// </summary>
        /// <param name="msec">mili-second</param>
        /// <param naem="func">called function after specified time</param>
        private static async void executeTask(int msec, Action func)
        {
            await Task.Delay(msec);
            func();
        }
    }
}