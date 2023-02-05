#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.TileMap
{
    public class MagicScript : Controller
    {
        private  AnimationTile? _animation = null;

        private Vector3 _direction = Vector3.Zero;
        private float _speed = 0.0f;

        public override void Start()
        {
            _animation = element.GetComponent<AnimationTile>();
            element.Disabled = true;
        }

        public override void Update(double elapsed)
        {
            if (_animation.StopAnimation && element.Disabled == false)
            {
                Log.Debug("animation is over. disable magic element");
                element.Disabled = true;
            }
            else
            {
                element.AddPosition(_direction * _speed * (float)elapsed);
            }
        }

        public void StartAnimation(Vector3 position, Vector3 direction, float speed)
        {
            _direction = direction;
            _speed = speed;
            element.Position = position;
            element.Disabled = false;
            _animation.RestartAnimation(true);
        }
    }
}