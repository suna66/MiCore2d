using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Collision
{
    public class StartScene : GameScene
    {
        ImageSprite awe;
        PlainSprite plain;
        public StartScene()
        {
             
        }

        public override void Load()
        {
            awe = new ImageSprite("../resource/awesomeface.png", 4);
            plain = new PlainSprite(4);
            plain.Alpha = 0.5f;
            plain.SetColor(1.0f, 0.0f, 0.0f);
            plain.RelationElement = awe;
            AddElement("awe", awe);
            AddElement("plain", plain);
        }

 
        public override void Update(double elapsed)
        {
            if (KeyStateInfo.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }

            awe.RadianZ = (float)CurrentTime;
            plain.CopyPositions(awe);
            Vector2[] vertix = awe.Vertix;
            MousePosition mouse = MousePositionInfo;
            if (CollisionUtil.PointPoly(vertix, mouse.Position))
            {
                plain.SetColor(0.0f, 0.0f, 1.0f);
            }
            else
            {
                plain.SetColor(1.0f, 0.0f, 0.0f);
            }
        }
    }
}
