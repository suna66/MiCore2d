#nullable disable warnings
using OpenTK.Mathematics;
using System.Collections;

namespace MiCore2d
{
    /// <summary>
    /// Controller. Base class of controller of element.
    /// </summary>
    public abstract class Controller : Component
    {
        /// <summary>
        ///  _start. status of called start method.
        /// </summary>
        private bool _start = false;


        /// <summary>
        ///  constructor.
        /// </summary>
        public Controller()
        {
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
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

        /// <summary>
        /// Start. Abstruct method. Callded first time of Update function.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Update. Abstruct method. Callded this method  each frame time.
        /// </summary>
        /// <param name="elapsed">elpased time of frame</param>
        public abstract void Update(double elapsed);

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}