using System;
using System.Reflection;
using System.IO;

namespace MiCore2d
{
    /// <summary>
    ///   Resources. resource accesser class
    /// </summary>
    public class Resources
    {
        /// <summary>
        /// ReadStream
        /// </summary>
        /// <param name="path">resource path</param>
        /// <returns>data stream</returns>
        public static Stream ReadStream(string path)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream? stream = assembly.GetManifestResourceStream(path);
            return stream ?? throw new FileNotFoundException($"Not Found Resource {path}");
        }

        /// <summary>
        /// ReadText
        /// </summary>
        /// <param name="path">resource path</param>
        /// <returns>string data</returns>
        public static string ReadText(string path)
        {
            using(Stream stream = ReadStream(path))
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        public static void Debug()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var name in assembly.GetManifestResourceNames())
            {
                Console.WriteLine($"Name: {name}");
                var stream = assembly.GetManifestResourceStream(name);
                var streamReader = new StreamReader(stream!);
                var text = streamReader.ReadToEnd();
                Console.Write(text);
            }
        }
    }
}