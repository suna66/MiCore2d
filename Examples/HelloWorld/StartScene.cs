using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example
{
    public class StartScene : GameScene
    {
        public StartScene()
        {
             
        }

        public override void Load()
        {
            LoadTexture2d("awe", "awesomeface.png");
            AddBasicSprite("awe", "awe", 3);

            LineSprite lineSprite = (LineSprite)AddElement("line", new LineSprite());
            lineSprite.SetColor(1.0f, 1.0f, 0.0f);
            lineSprite.SetLine(new Vector3(-7.0f, 4.0f, 0.0f), new Vector3(7.0f, 4.0f, 0.0f));

            SceneCanvas.SetColor(255, 255, 255);
            SceneCanvas.SetFontSize(24);
            SceneCanvas.DrawString( 10, 10, "Hello World MiCore2d!");
            SceneCanvas.Flush();
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
