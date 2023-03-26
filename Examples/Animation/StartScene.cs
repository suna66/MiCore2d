#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Animation
{
    public class StartScene : GameScene
    {
        private float blur = 0.0f;
        private float step = 0.05f;
        BlurRenderer renderer = null!;
        
        public override void Load()
        {
            ImageSprite sprite = new ImageSprite("../resource/GirlTile001.png", 32, 32, 1);
            sprite.AddComponent<PlayerScript>();

            ImageSprite awe = new ImageSprite("../resource/awesomeface.png", 3);
            awe.SetPosition(0.0f, -3.0f, 0.0f);
            awe.AddComponent<BoxCollider>();
            awe.AddComponent<HardBody>();

            ImageSprite backgorund = new ImageSprite("../resource/park.jpg", 10);
            renderer = new BlurRenderer();
            renderer.Blur = blur;
            renderer.Bloom = 1.0f;
            backgorund.SetRenderer(renderer);

            AddElement("back", backgorund);
            AddElement("awe", awe);
            AddElement("girl", sprite);
        }

         public override void Update(double elapsed)
         {
            if (KeyState.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }
            blur += step;
            if (blur >= 30.0f)
            {
                step = -0.05f;
            }
            if (blur <= 0.0f)
            {
                blur = 0.0f;
                step = 0.05f;
            }
            renderer.Blur = blur;
         }
    }
}
