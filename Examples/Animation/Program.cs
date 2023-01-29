using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Animation
{
    public class Program
    {
        private static void Main()
        {
            GameMain main = new GameMain("Animation", 800, 600);
            main.ClearColor = Color4.Black;

            main.Run(new StartScene());
        }
    }
}