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
            _animation.Interval = 0.05f;
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
            CollisionDetector();
        }

        public void StartAnimation(Vector3 position, Vector3 direction, float speed)
        {
            _direction = direction;
            _speed = speed;
            element.Position = position;
            element.Disabled = false;
            _animation.RestartAnimation(true);
        }

        public override void OnEnterCollision(Element target)
        {
            if (element.Disabled)
            {
                return;
            }
            if (target.Name == "awe")
            {
                target.Disabled = true;
                Element explosion = gameScene.GetElement("explosion");
                explosion.Position = target.Position;
                explosion.Disabled = false;
                AnimationTile animation = explosion.GetComponent<AnimationTile>();
                animation.Interval = 0.3f;
                animation.RestartAnimation(true);
                gameScene.Audio.Play("explosion");
                TimeUtil.Delay(1000, offExplosionSprite);
            }
        }

        private void offExplosionSprite()
        {
            Element explosion = gameScene.GetElement("explosion");
            explosion.Disabled = true;
        }
    }
}