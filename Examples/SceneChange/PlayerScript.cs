#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.SceneChange
{
    public class PlayerScript : Controller
    {
        private AnimationTile animation;
        private FadeScript? fadeScript = null;

        public override void Start()
        {
            element.AddComponent<RigidBody>();
            animation = element.AddComponent<AnimationTile>();
            animation.AddPattern("down", new int[] {0, 1, 2} );
            animation.AddPattern("left", new int[] {3, 4, 5} );
            animation.AddPattern("right", new int[] {6, 7, 8} );
            animation.AddPattern("up", new int[] {9, 10, 11} );
            animation.SwitchPattern("down");
            animation.StopAnimation = true;

            element.AddComponent<BoxCollider>();

            fadeScript = gameScene.GetElement("fade").GetComponent<FadeScript>();
            //IsEnableCollsionDetect = true;
        }

        public override void Update(double elapsed)
        {
            if (fadeScript.IsAnimation)
                return;
            
            bool isKeyDown = false;
            
            var input = gameScene.KeyStateInfo;

            if (input.IsKeyDown(Keys.Up))
            {
                animation.SwitchPattern("up");
                element.AddPositionY(5 * (float)elapsed);
                isKeyDown = true;
            }
            if (input.IsKeyDown(Keys.Down))
            {
                animation.SwitchPattern("down");
                element.AddPositionY(-5 * (float)elapsed);
                isKeyDown = true;
            }
            if (input.IsKeyDown(Keys.Left))
            {
                animation.SwitchPattern("left");
                element.AddPositionX(-5 * (float)elapsed);
                isKeyDown = true;
            }
            if (input.IsKeyDown(Keys.Right))
            {
                animation.SwitchPattern("right");
                element.AddPositionX(5 * (float)elapsed);
                isKeyDown = true;
            }
            if (isKeyDown)
            {
                if (animation.StopAnimation)
                {
                    animation.StopAnimation = false;
                }
            }
            else
            {
                animation.StopAnimation = true;
            }
        }

        public override void OnEnterCollision(CollisionInfo collision)
        {
            if (collision.target.Name == "awe")
            {
                animation.StopAnimation = true;
                fadeScript.SwitchScene(new NextScene());
            }
        }

    }
}