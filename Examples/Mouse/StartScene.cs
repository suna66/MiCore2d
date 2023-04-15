using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Mouse
{
    public class StartScene : GameScene
    {
        private Vector2 mouse = Vector2.Zero;

        private PlainSprite? rect;
        private ImageSprite? awe;
        private ImageSprite? moveAwe;

        private BasicButton? rectBtn;
        private BasicButton? aweBtn;

        private int direction = 1;

        public StartScene()
        {
             
        }

        public override void Load()
        {
            Audio.LoadMP3File("magic", "../resource/magic.mp3", false);

            awe = new ImageSprite("../resource/awesomeface.png", 1);
            awe.AddComponent<CircleCollider>();
            awe.Position = new Vector3(3.0f, 1.0f, 0.0f);
            aweBtn = awe.AddComponent<BasicButton>();
            aweBtn.MouseEnter = () => {
                awe.Alpha = 0.5f;
            };
            aweBtn.MouseLeave = () => {
                awe.Alpha = 1.0f;
            };

            moveAwe = new ImageSprite("../resource/awesomeface.png", 1);
            moveAwe.Position = new Vector3(0.0f, 1.0f, 0.0f);

            rect = new PlainSprite(1);
            rect.Position = new Vector3(-3.0f, 1.0f, 0.0f);
            rect.SetColor(0.1f, 0.5f, 0.0f);
            rect.AddComponent<BoxCollider>();
            rectBtn = rect.AddComponent<BasicButton>();
            rectBtn.ButtonDown = () => {
                rect.SetColor(1.0f, 0.0f, 0.0f);
                Audio.Play("magic");
            };
            rectBtn.ButtonUp = () => {
                rect.SetColor(0.1f, 0.5f, 0.0f);
            };
            AddElement("rect", rect);
            AddElement("awe", awe);
            AddElement("moveAwe", moveAwe);
        }

        private void moveAweSprite(double elapsed)
        {
            Vector2 dir = Vector2.UnitX;
            if (direction == 1)
            {
                moveAwe?.AddPositionX((float)elapsed * 1.5f);
                dir = Vector2.UnitX;
            }
            else
            {
                moveAwe?.AddPositionX((float)elapsed * 1.5f * -1);
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

        public override void Update(double elapsed)
        {
            if (KeyStateInfo.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            moveAweSprite(elapsed);
        }
    }
}
