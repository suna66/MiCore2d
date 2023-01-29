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
            LoadTexture2dTile("girl", "../resource/GirlTile001.png", 32, 32);
            LoadTexture2d("awe", "../resource/awesomeface.png");
            Element sprite = AddAnimationSprite("girl", "girl", 1);
            sprite.AddComponent<PlayerScript>();

            Element awe = AddBasicSprite("awe", "awe", 3);
            awe.SetPosition(0.0f, -3.0f, 0.0f);
            awe.AddComponent<BoxCollider>();
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
