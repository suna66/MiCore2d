using OpenTK.Windowing.GraphicsLibraryFramework;
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
