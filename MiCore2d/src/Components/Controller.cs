#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public abstract class Controller : Component
    {
        private bool _start = false;

        public Controller()
        {
        }

        public override void UpdateComponent(double elapsed)
        {
            if (!_start)
            {
                Start();
                _start = true;
            }
            else
            {
                Update(elapsed);
            }
        }

        public virtual void OnEnterCollision(Element target)
        {
        }

        public virtual void OnStayCollision(Element target)
        {
        }

        public virtual void OnLeaveCollision(Element target)
        {
        }

        public abstract void Start();

        public abstract void Update(double elapsed);

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}