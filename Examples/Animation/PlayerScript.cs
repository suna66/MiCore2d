#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Animation
{
    public class PlayerScript : Controller
    {
        private AnimationTile animation;

         private HardBody body = null!;

        public override void Start()
        {
            animation = element.AddComponent<AnimationTile>();
            animation.AddPattern("down", new int[] {0, 1, 2} );
            animation.AddPattern("left", new int[] {3, 4, 5} );
            animation.AddPattern("right", new int[] {6, 7, 8} );
            animation.AddPattern("up", new int[] {9, 10, 11} );
            animation.SwitchPattern("down");

            body = gameScene.GetElement("awe").GetComponent<HardBody>();
            BoxCollider collider = element.AddComponent<BoxCollider>();
            collider.ArrangePosition = new Vector3(0.0f, -0.25f, 0.0f);
            collider.HeightUnit = 0.5f;
            IsEnableCollsionDetect = true;
        }

        public override void Update(double elapsed)
        {
            var input = gameScene.KeyState;

            if (input.IsKeyDown(Keys.Up))
            {
                animation.SwitchPattern("up");
                element.AddPositionY(5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Down))
            {
                animation.SwitchPattern("down");
                element.AddPositionY(-5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Left))
            {
                animation.SwitchPattern("left");
                element.AddPositionX(-5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Right))
            {
                animation.SwitchPattern("right");
                element.AddPositionX(5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Space))
            {
                body.Shake(0.05f, 1000);
            }
        }

    }
}