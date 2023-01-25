using System;
using System.Reflection;
using System.IO;

namespace MiCore2d
{
    public class Resources
    {
        private Resources()
        {

        }

        public static Stream ReadStream(string path)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream? stream = assembly.GetManifestResourceStream(path);
            return stream ?? throw new FileNotFoundException($"Not Found Resource {path}");
        }

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