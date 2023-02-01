using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.HelloWorld
{
    public class StartScene : GameScene
    {
        public StartScene()
        {
             
        }

        public override void Load()
        {
            ImageSprite awe = new ImageSprite(LoadTexture2d("awe", "../resource/awesomeface.png"), 3);
            
            LineSprite lineSprite = new LineSprite();
            lineSprite.SetColor(1.0f, 1.0f, 0.0f);
            lineSprite.SetLine(new Vector3(-4.0f, 4.0f, 0.0f), new Vector3(4.0f, 4.0f, 0.0f));

            PlainSprite rect = new PlainSprite(1);
            rect.Alpha = 0.4f;
            rect.Position = new Vector3(0.0f, 4.0f, 0.0f);
            rect.SetScaleX(14.0f);
            rect.SetScaleY(2.0f);
            rect.SetColor(0.1f, 0.5f, 0.0f);

            AddElement("aws", awe);
            AddElement("line", lineSprite);
            AddElement("rect", rect);

            SceneCanvas.SetColor(255, 255, 255);
            SceneCanvas.SetFontSize(24);
            SceneCanvas.DrawString( 10, 10, "Hello World MiCore2d!");
            SceneCanvas.Flush();

            TimeUtil.Delay(1000, debugMsg);
        }

        private void debugMsg()
        {
            Log.Info("output logs");
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
