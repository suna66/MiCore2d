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
        private const int ACTION_TYPE_NONE = 0;

        private const int ACTION_TYPE_FORCE = 1;

        private const int ACTION_TYPE_SHAKE = 2;
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
        /// Amplitude.
        /// </summary>
        /// <value></value>
        public float Amplitude {get; set;}

        /// <summary>
        /// Layer. layer name of collision.
        /// </summary>
        /// <value></value>
        public string Layer {get; set;} = "default";


        private int actionType = ACTION_TYPE_NONE;

        /// <summary>
        /// AddForce. Add force of moving this element.
        /// </summary>
        /// <param name="directPower">direction of power</param>
        /// <param name="msec">time of keeping power</param>
        public void AddForce(Vector3 directPower, float msec)
        {
            Timeout = msec/1000.0f;
            Power = directPower;
            actionType = ACTION_TYPE_FORCE;
        }

        public void Shake(float amplitude, float msec)
        {
            Timeout = msec/1000.0f;
            Amplitude = amplitude;
            actionType = ACTION_TYPE_SHAKE;
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            if (Timeout > 0.0)
            {
                if (actionType == ACTION_TYPE_FORCE)
                {
                    element.Position += Power;
                } else if (actionType == ACTION_TYPE_SHAKE)
                {
                    Random r = new Random();
                    float x = (float)(r.NextDouble() * 2 - 1) * Amplitude;
                    float y = (float)(r.NextDouble() * 2 - 1) * Amplitude;
                    element.Position += new Vector3(x, y, 0);
                }
                Timeout -= (float)elapsed;
            }
            else
            {
                actionType = ACTION_TYPE_NONE;
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