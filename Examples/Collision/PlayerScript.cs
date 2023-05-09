#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.Collision
{
    public class PlayerScript : Controller
    {
        private Gravity? gravity;
        private float _timeout = 0.0f;
        public override void Start()
        {
            //IsEnableCollsionDetect = true;
            //gravity = element.GetComponent<Gravity>();
        }

        public override void Update(double elapsed)
        {
            var input = gameScene.KeyStateInfo;

            if (input.IsKeyDown(Keys.Up))
            {
                element.AddPositionY(5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Down))
            {
                element.AddPositionY(-5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Left))
            {
                element.AddPositionX(-5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Right))
            {
                element.AddPositionX(5 * (float)elapsed);
            }
            if (input.IsKeyDown(Keys.Space))
            {
                // if (_timeout < 0.0f)
                // {
                //     gravity.AddForce(new Vector3(0.0f, 6.0f, 0.0f));
                // }
                // _timeout = 0.1f;
            }
            _timeout -= (float)elapsed;
        }

        // public override void OnEnterCollision(Element target)
        // {
        //     target.Alpha = 0.4f;
        // }

        // public override void OnLeaveCollision(Element target)
        // {
        //     target.Alpha = 1.0f;
        // }
    }
}
