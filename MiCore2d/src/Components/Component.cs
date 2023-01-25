#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public abstract class Component
    {
        protected Element? element = null;

        private bool _destroyed = false;

        public Component()
        {
        }

        public virtual void OnLoad()
        {
        }

        public bool Destroyed
        {
            get => _destroyed;
            set
            {
                _destroyed = value;
            }
        }

        public void SetParent(Element e)
        {
            element = e;
        }

        public virtual void UpdateComponent(double elapsed)
        {

        }

        public virtual void Dispose()
        {
            element = null;
        }

        public GameScene gameScene
        {
            get => element.GetParentGameScene();
        }
    }
}