#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.SceneChange
{
    public class StartScene : GameScene
    {
        private FadeScript? fadeScript = null;
        public override void Load()
        {
            // LoadTexture2dTile("girl", "../resource/GirlTile001.png", 32, 32);
            // LoadTexture2d("awe", "../resource/awesomeface.png");

            //ImageSprite sprite = AddImageSprite("girl", "girl", 1);
            ImageSprite sprite = new ImageSprite(LoadTexture2dTile("girl", "../resource/GirlTile001.png", 32, 32), 1);
            sprite.AddComponent<PlayerScript>();

            //ImageSprite awe = AddImageSprite("awe", "awe", 1);
            ImageSprite awe = new ImageSprite(LoadTexture2d("awe", "../resource/awesomeface.png"), 1);
            awe.SetPosition(0.0f, -3.0f, 0.0f);
            awe.AddComponent<BoxCollider>();

            //PlainSprite fade = AddPlainSprite("fade", 1);
            PlainSprite fade = new PlainSprite(1);
            fade.SetScaleX(14.0f);
            fade.SetScaleY(10.0f);
            fade.SetColor(0.0f, 0.0f, 0.0f);
            fadeScript = fade.AddComponent<FadeScript>();

            AddElement("girl", sprite);
            AddElement("awe", awe);
            AddElement("fade", fade);

            SceneCanvas.SetColor(255, 255, 0);
            SceneCanvas.SetFontSize(24);
            SceneCanvas.DrawString( 10, 10, "Press Space Key to change scene");
            SceneCanvas.Flush();
        }

         public override void Update(double elapsed)
         {
            if (IsFadeAnimation())
                return;

            if (KeyState.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            if (KeyState.IsKeyDown(Keys.Space))
            {
                fadeScript.SwitchScene(new NextScene());
            }
         }

         public bool IsFadeAnimation()
         {
            return fadeScript.IsAnimation;
         }
    }
}
