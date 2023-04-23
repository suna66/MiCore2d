#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Collision
{
    public class PlayerScript : Controller
    {

        public override void Start()
        {
            IsEnableCollsionDetect = true;
        }

        public override void Update(double elapsed)
        {
            var input = gameScene.KeyStateInfo;

            if (input.IsKeyDown(Keys.Up))
            {
                element.AddPositionY(5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Down))
            {
                element.AddPositionY(-5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Left))
            {
                element.AddPositionX(-5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Right))
            {
                element.AddPositionX(5 * (float)elapsed);
            }
        }
    }
}
