#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.SceneChange
{
    public class  NextScene : GameScene
    {
        private FadeScript? fadeScript = null;
        public override void Load()
        {
            LoadTexture2dTile("girl", "../resource/GirlTile001.png", 32, 32);
            LoadTexture2d("awe", "../resource/awesomeface.png");
            Element sprite = AddImageSprite("girl", "girl", 1);
            sprite.AddComponent<PlayerScript>();

            Element awe = AddImageSprite("awe", "awe", 3);
            BoxCollider collider = awe.AddComponent<BoxCollider>();
            collider.IsDynamic = true;

            PlainSprite fade = (PlainSprite)AddPlainSprite("fade", 1);
            fade.SetScaleX(14.0f);
            fade.SetScaleY(10.0f);
            fade.SetColor(0.0f, 0.0f, 0.0f);
            fadeScript = fade.AddComponent<FadeScript>();

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
                fadeScript.SwitchScene(new StartScene());
            }
            Element element = GetElement("awe");
            element.SetScale(2 * MathF.Cos((float)CurrentTime));
         }

         public bool IsFadeAnimation()
         {
            return fadeScript.IsAnimation;
         }
    }
}
