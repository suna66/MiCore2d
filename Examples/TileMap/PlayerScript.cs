#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.TileMap
{
    public class PlayerScript : Controller
    {
        private AnimationTile animation;

        public override void Start()
        {
            animation = element.AddComponent<AnimationTile>();
            animation.AddPattern("down", new int[] {0, 1, 2} );
            animation.AddPattern("left", new int[] {3, 4, 5} );
            animation.AddPattern("right", new int[] {6, 7, 8} );
            animation.AddPattern("up", new int[] {9, 10, 11} );
            animation.SwitchPattern("down");

            element.AddComponent<HardBody>();
            element.AddComponent<BoxCollider>();
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
                MagicScript magic = gameScene.GetElement("magic").GetComponent<MagicScript>();
                Vector3 direct = Vector3.Zero;
                if (animation.AnimationName == "up")
                {
                    direct = Vector3.UnitY;
                }
                else if (animation.AnimationName == "down")
                {
                    direct = -1 * Vector3.UnitY;
                }
                else if (animation.AnimationName == "left")
                {
                    direct = -1 * Vector3.UnitX;
                }
                else if (animation.AnimationName == "right")
                {
                    direct = Vector3.UnitX;
                }
                magic.StartAnimation(element.Position, direct, 2.0f);
                gameScene.Audio.Play("magic");
            }
        }

        public override void OnEnterCollision(Element target)
        {
            if (target.Name == "awe")
            {
                target.Disabled = true;
                Element explosion = gameScene.GetElement("explosion");
                explosion.Position = target.Position;
                explosion.Disabled = false;
                AnimationTile animation = explosion.GetComponent<AnimationTile>();
                animation.Interval = 0.3f;
                animation.RestartAnimation(true);
                gameScene.Audio.Play("explosion");
                TimeUtil.Delay(1000, offExplosionSprite);
            }
        }

        private void offExplosionSprite()
        {
            Element explosion = gameScene.GetElement("explosion");
            explosion.Disabled = true;
        }

    }
}