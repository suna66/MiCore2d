using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;
using SkiaSharp;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            
            CanvasSprite can = new CanvasSprite(1500, 240, 2);
            can.Position = new Vector3(0.0f, -4.0f, 0.0f);
            SKCanvas skCanvas = can.GetCanvas();
            //skCanvas.Clear(new SKColor(0, 0, 255, 255));
            CanvasUtil.Clear(skCanvas, CanvasUtil.Color(0, 0, 0, 64));
            CanvasUtil.DrawString(skCanvas, 0, 0, "Hello", CanvasUtil.Color(255, 255, 255, 255), 50);
            can.Flush();

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

            ////IV(16 charactor words)
            //string iv = "f0321tkmw5E0jb8h";
            ////Key(32 charactor words)
            //string key = "OS6ynCOaSdRODItHeJ4yPkSd9V7147J3";

            //using (Stream stream = File.Open("Program.cs", FileMode.Open))
            //{
            //    EncryptUtil.EncryptFile("Program.enc", stream, iv, key);

            //    using (Stream readStream = EncryptUtil.DecryptFile("Program.enc", iv, key))
            //    {
            //        Log.Debug($"{readStream.Length}");
            //        byte[] bytes = (readStream as MemoryStream).ToArray();
            //        string text = System.Text.Encoding.ASCII.GetString(bytes);
            //        Log.Debug(text);
            //    }
            //}
        }

         public override void Update(double elapsed)
         {
            if (KeyStateInfo.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }
         }
    }
}
