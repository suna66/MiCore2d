using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;
using SkiaSharp;

namespace Example.HelloWorld
{
    public class StartScene : GameScene
    {
        public StartScene()
        {
             
        }

        public override void Load()
        {
            ImageSprite awe = new ImageSprite("../resource/awesomeface.png", 3);
            
            CanvasSprite can = new CanvasSprite(320, 240, 2);
            can.Position = new Vector3(-2.0f, 0.0f, 0.0f);
            SKCanvas skCanvas = can.GetCanvas();
            skCanvas.Clear(new SKColor(0, 255, 0, 128));

            PlainSprite rect = new PlainSprite(1);
            rect.Alpha = 0.4f;
            rect.Position = new Vector3(0.0f, 4.0f, 0.0f);
            rect.SetScaleX(14.0f);
            rect.SetScaleY(2.0f);
            rect.SetColor(0.1f, 0.5f, 0.0f);

            AddElement("aws", awe);
            AddElement("can", can);
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
