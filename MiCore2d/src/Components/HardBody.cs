#nullable disable warnings
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;

namespace MiCore2d
{
    /// <summary>
    /// HardBody. Thie component make element solided object.
    /// </summary>
    public class HardBody : Component
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public HardBody()
        {
            Timeout = 0;
            Power = Vector3.Zero;
        }

        /// <summary>
        /// Timeout. timeout of action.
        /// </summary>
        /// <value></value>
        public float Timeout {get; set;}

        /// <summary>
        /// Power. direction and power value.
        /// </summary>
        /// <value>power</value>
        public Vector3 Power {get; set;}

        /// <summary>
        /// Layer. layer name of collision.
        /// </summary>
        /// <value></value>
        public string Layer {get; set;} = "default";

        /// <summary>
        /// AddForce. Add force of moving this element.
        /// </summary>
        /// <param name="directPower">direction of power</param>
        /// <param name="msec">time of keeping power</param>
        public void AddForce(Vector3 directPower, float msec)
        {
            Timeout = msec/1000.0f;
            Power = directPower;
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            if (Timeout > 0.0)
            {
                element.Position += Power;
                Timeout -= (float)elapsed;
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}