using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Collision
{
    public class StartScene : GameScene
    {
        ImageSprite? awe;
        ImageSprite? awe0;
        PlainSprite? plain;
        public StartScene()
        {
             
        }

        public override void Load()
        {
            awe0 = new ImageSprite("../resource/awesomeface.png", 2);
            awe0.AddComponent<BoxCollider>();
            awe0.AddComponent<Gravity>();
            awe0.AddComponent<PlayerScript>();
            awe0.Position = new Vector3(1.0f, 4.0f, 0.0f);
            awe = new ImageSprite("../resource/awesomeface.png", 4);
            awe.AddComponent<CircleCollider>();
            awe.Position = new Vector3(1.0f, -5.0f, 0.0f);
            plain = new PlainSprite(4);
            plain.Alpha = 0.5f;
            plain.SetColor(1.0f, 0.0f, 0.0f);
            plain.RelationElement = awe;

            BlankSprite blank = new BlankSprite(2);
            blank.Position = new Vector3(-2.0f, -5.0f, 0.0f);
            blank.AddComponent<BoxCollider>();

            AddElement("awe", awe);
            AddElement("awe0", awe0);
            AddElement("plain", plain);
            AddElement("blank", blank);
        }

 
        public override void Update(double elapsed)
        {
            if (KeyStateInfo.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            // awe.RadianZ = (float)CurrentTime;
            // plain.CopyPositions(awe);
            // Vector2[] vertix = awe.Vertix;
            // MousePosition mouse = MousePositionInfo;
            // if (CollisionUtil.PointPoly(vertix, mouse.Position))
            // {
            //     plain.SetColor(0.0f, 0.0f, 1.0f);
            // }
            // else
            // {
            //     plain.SetColor(1.0f, 0.0f, 0.0f);
            // }
        }
    }
}
