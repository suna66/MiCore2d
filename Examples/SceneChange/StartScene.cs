#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;
using MiCore2d.Audio;

namespace Example.SceneChange
{
    public class StartScene : GameScene
    {
        private FadeScript? fadeScript = null;
        public override void Load()
        {
            if (!Audio.IsPlay("sound1"))
            {
                if (!Audio.IsLoaded("sound1"))
                {
                    Audio.LoadMP3File("sound1", "../resource/sanjinooyatsu.mp3", true);
                }
                Audio.Play("sound1");
            }
            ImageSprite sprite = new ImageSprite(LoadTexture2dTile("girl", "../resource/GirlTile001.png", 32, 32), 1);
            sprite.AddComponent<PlayerScript>();

            ImageSprite awe = new ImageSprite(LoadTexture2d("awe", "../resource/awesomeface.png"), 1);
            awe.SetPosition(0.0f, -3.0f, 0.0f);
            Collider awe_collider = awe.AddComponent<BoxCollider>();
            awe_collider.IsTrigger = true;

            PlainSprite fade = new PlainSprite(1);
            fade.SetScaleX(14.0f);
            fade.SetScaleY(10.0f);
            fade.SetColor(0.0f, 0.0f, 0.0f);
            fadeScript = fade.AddComponent<FadeScript>();

            AddElement("girl", sprite);
            AddElement("awe", awe);
            AddElement("fade", fade);

            SceneCanvas.SetColor(255, 255, 255);
            SceneCanvas.SetFontSize(24);
            SceneCanvas.DrawString( 10, 10, "Hit to Awesomeface image!");
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
         }

         public bool IsFadeAnimation()
         {
            return fadeScript.IsAnimation;
         }
    }
}
