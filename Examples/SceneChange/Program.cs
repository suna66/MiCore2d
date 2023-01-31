using OpenTK.Mathematics;
using MiCore2d;

namespace Example.SceneChange
{
    public class Program
    {
        private static void Main()
        {
            GameMain main = new GameMain("Scnene Change", 800, 600);
            main.ClearColor = Color4.Blue;

            main.Run(new StartScene());
        }
    }
}