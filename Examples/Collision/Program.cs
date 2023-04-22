using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Collision
{
    public class Program
    {
        private static void Main()
        {
            GameMain main = new GameMain("CollisionTest", 800, 600);
            main.ClearColor = Color4.Navy;

            main.Run(new StartScene());
        }
    }
}
