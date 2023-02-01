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
            ImageSprite sprite = new ImageSprite(LoadTexture2dTile("girl", "../resource/GirlTile001.png", 32, 32), 1);
            sprite.AddComponent<PlayerScript>();

            ImageSprite awe = new ImageSprite(LoadTexture2d("awe", "../resource/awesomeface.png"), 3);
            awe.SetPosition(0.0f, -3.0f, 0.0f);
            awe.AddComponent<BoxCollider>();

            AddElement("girl", sprite);
            AddElement("awe", awe);
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
