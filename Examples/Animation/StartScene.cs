#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Animation
{
    public class StartScene : GameScene
    {
        public override void Load()
        {
            ImageSprite sprite = new ImageSprite("../resource/GirlTile001.png", 32, 32, 1);
            sprite.AddComponent<PlayerScript>();

            ImageSprite awe = new ImageSprite("../resource/awesomeface.png", 3);
            awe.SetPosition(0.0f, -3.0f, 0.0f);
            awe.AddComponent<BoxCollider>();
            awe.AddComponent<HardBody>();

            AddElement("awe", awe);
            AddElement("girl", sprite);
        }

         public override void Update(double elapsed)
         {
            if (KeyState.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }
         }
    }
}
