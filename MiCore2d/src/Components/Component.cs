#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Component. Base class of Component.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// element. the element attended this component.
        /// </summary>
        protected Element? element = null;

        /// <summary>
        /// _destoryed.
        /// </summary>
        private bool _destroyed = false;

        /// <summary>
        /// constructor.
        /// </summary>
        public Component()
        {
        }

        /// <summary>
        /// OnLoad. Initialize Component.
        /// </summary>
        public virtual void OnLoad()
        {
        }

        /// <summary>
        /// Destroyed. Manage the status of destroyed or not.
        /// </summary>
        /// <value>true: destroyed, false: live</value>
        public bool Destroyed
        {
            get => _destroyed;
            set
            {
                _destroyed = value;
            }
        }

        /// <summary>
        /// SetParent. setting element attached the component.
        /// </summary>
        /// <param name="e"></param>
        public void SetParent(Element e)
        {
            element = e;
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public virtual void UpdateComponent(double elapsed)
        {

        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            element = null;
        }

        /// <summary>
        /// gameScene. getting game scene instance of the element attached this component is belonging to.
        /// </summary>
        /// <value>game scene</value>
        public GameScene gameScene
        {
            get => element.CurrentGameScene;
        }
    }
}