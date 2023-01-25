// using OpenTK.Windowing.Common;
// using OpenTK.Windowing.Desktop;
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
            AddBasicSprite("awe", "awe", 1);
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
