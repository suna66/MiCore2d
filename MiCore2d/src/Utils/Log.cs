using System;
using System.Diagnostics;

namespace MiCore2d
{
    /// <summary>
    /// Log.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="message">debug message</param>
        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            Console.WriteLine($"[DEBUG] {message}");
        }

        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="message">information message</param>
        [Conditional("DEBUG")]
        public static void Info(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }

        /// <summary>
        /// Warn.
        /// </summary>
        /// <param name="message">warning message</param>
        [Conditional("DEBUG")]
        public static void Warn(string message)
        {
            Console.WriteLine($"[WARN] {message}");
        }

        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="message">error message</param>
        public static void Error(string message)
        {
            Console.WriteLine($"[ERROR] {message}");
        }
    }
}