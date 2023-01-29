using System;
using System.Diagnostics;

namespace MiCore2d
{
    public class Log
    {
        [Conditional("DEBUG")]
        public static void Debug(string message)
        {
            Console.WriteLine($"[DEBUG] {message}");
        }

        [Conditional("DEBUG")]
        public static void Info(string message)
        {
            Console.WriteLine($"[INFO] {message}");
        }

        [Conditional("DEBUG")]
        public static void Warn(string message)
        {
            Console.WriteLine($"[WARN] {message}");
        }

        public static void Error(string message)
        {
            Console.WriteLine($"[ERROR] {message}");
        }
    }
}