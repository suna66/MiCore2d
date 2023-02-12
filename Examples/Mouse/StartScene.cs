using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Mouse
{
    public class StartScene : GameScene
    {
        private Vector2 mouse = Vector2.Zero;

        private PlainSprite rect;
        private ImageSprite awe;
        private ImageSprite moveAwe;

        private bool hitRect = false;
        private bool hitAwe = false;

        private int direction = 1;

        public StartScene()
        {
             
        }

        public override void Load()
        {
            Texture2d tex = LoadTexture2d("awe", "../resource/awesomeface.png");
            awe = new ImageSprite(tex, 1);
            awe.AddComponent<CircleCollider>();
            awe.Position = new Vector3(3.0f, 1.0f, 0.0f);

            moveAwe = new ImageSprite(tex, 1);
            moveAwe.Position = new Vector3(0.0f, 1.0f, 0.0f);

            rect = new PlainSprite(1);
            rect.Position = new Vector3(-3.0f, 1.0f, 0.0f);
            rect.SetColor(0.1f, 0.5f, 0.0f);
            rect.AddComponent<BoxCollider>();
            AddElement("rect", rect);
            AddElement("awe", awe);
            AddElement("moveAwe", moveAwe);
        }

        private void moveAweSprite(double elapsed)
        {
            Vector2 dir = Vector2.UnitX;
            if (direction == 1)
            {
                moveAwe.AddPositionX((float)elapsed * 1.5f);
                dir = Vector2.UnitX;
            }
            else
            {
                moveAwe.AddPositionX((float)elapsed * 1.5f * -1);
                dir = -1 * Vector2.UnitX;
            }

            Element e = Physics.Raycast(moveAwe.Position, dir, moveAwe.Unit/2);
            if (e != null)
            {
                if (direction == 1)
                {
                    direction = -1;
                }
                else
                {
                    direction = 1;
                }
            }
        }

        private void hitSprites()
        {
            bool isHitRect = false;
            bool isHitAwe = false;

            Element e = Physics.Pointcast(mouse);
            if (e != null)
            {
                if (e.Name == "rect")
                {
                    PlainSprite sprite = (PlainSprite)e;
                    sprite.SetColor(1.0f, 0.0f, 0.0f);
                    isHitRect = true;
                }
                else if (e.Name == "awe")
                {
                    Log.Debug("awe hit");
                    awe.Alpha = 0.5f;
                    isHitAwe = true;
                }
            }

            if (isHitRect == false && hitRect)
            {
                rect.SetColor(0.1f, 0.5f, 0.0f);
            }
            if (isHitAwe == false && hitAwe)
            {
                awe.Alpha = 1.0f;
            }
            hitRect = isHitRect;
            hitAwe = isHitAwe;
        }

        public override void Update(double elapsed)
        {
            if (KeyState.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            moveAweSprite(elapsed);
            hitSprites();
        }

        public override void OnMouseButton(MouseButton button, bool pressed)
        {

        }

        public override void OnMouseMove(float x, float y, float deltaX, float deltaY)
        {
            mouse = LocalToWorld(new Vector2(x, y));
        }
    }
}
