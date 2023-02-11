using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Mouse
{
    public class StartScene : GameScene
    {
        private Vector2 mouse = Vector2.Zero;

        private PlainSprite rect;

        public StartScene()
        {
             
        }

        public override void Load()
        {
            ImageSprite awe = new ImageSprite(LoadTexture2d("awe", "../resource/awesomeface.png"), 1);
            awe.AddComponent<CircleCollider>();
            awe.Position = new Vector3(2.0f, 1.0f, 0.0f);

            rect = new PlainSprite(1);
            rect.Position = new Vector3(0.0f, 0.0f, 0.0f);
            rect.SetColor(0.1f, 0.5f, 0.0f);
            rect.AddComponent<BoxCollider>();
            AddElement("rect", rect);
            AddElement("awe", awe);
        }

        public override void Update(double elapsed)
        {
            if (KeyState.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            Element e = Physics.Pointcast(mouse);
            if (e != null)
            {
                if (e.Name == "rect")
                {
                    PlainSprite sprite = (PlainSprite)e;
                    sprite.SetColor(1.0f, 0.0f, 0.0f);
                }
                else if (e.Name == "awe")
                {
                    Log.Debug("awe hit");
                }
            }
            else
            {
                rect.SetColor(0.1f, 0.5f, 0.0f);
            }
        }

        public override void OnMouseButton(MouseButton button, bool pressed)
        {

        }

        public override void OnMouseMove(float x, float y, float deltaX, float deltaY)
        {
            mouse.X = x;
            mouse.Y = y;
        }
    }
}
