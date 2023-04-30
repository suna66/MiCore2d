#nullable disable warnings
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;

namespace MiCore2d
{
    struct GravityState
    {
        public Vector3 previousPosition;
        public float effectTime;
        public Vector3 force;

        public GravityState(Vector3 _pos, float _effectTime, Vector3 _force)
        {
            previousPosition = _pos;
            effectTime = _effectTime;
            force = _force;
        }
    }

    /// <summary>
    /// Gravity. emulate gravity components.
    /// </summary>
    public class Gravity : Component
    {
        /// <summary>
        /// _gravity.
        /// </summary>
        private float _gravity = 9.8f;

        /// <summary>
        /// effect state.
        /// </summary>
        private GravityState _state;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Gravity()
        {
            _state = new GravityState(Vector3.Zero, 0.0f, Vector3.Zero);
        }

        /// <summary>
        /// GravityElulationAdjust
        /// </summary>
        /// <value>float</value>
        public float GravityEmulationAdjust {get; set;} = 0.03f;

        /// <summary>
        /// SetGravity
        /// </summary>
        /// <param name="g">gravity value</param>
        public void SetGravity(float g)
        {
            _gravity = g;
        }

        /// <summary>
        /// AddForce. add force to element once.
        /// </summary>
        /// <param name="force"></param>
        public void AddForce(Vector3 force)
        {
            _state.force = force;
        }

 
        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            Vector3 currentPosition = element.Position;
            if (element.Position.Y >= _state.previousPosition.Y)
            {
                _state.effectTime = 0.0f;
            }

            if (_state.force.X != 0.0f || _state.force.Y != 0.0f)
            {
                element.AddPosition(_state.force * GravityEmulationAdjust);
                if (_state.force.X != 0.0f)
                {
                    float direct = _state.force.X;
                    _state.force.X -= (float)elapsed * _gravity;
                    if (direct > 0.0f)
                    {
                        if (_state.force.X <= 0.0f)
                        {
                            _state.force.X = 0.0f;
                        }
                    }
                    else
                    {
                        if (_state.force.X >= 0.0f)
                        {
                            _state.force.X = 0.0f;
                        }
                    }
           
                }
                if (_state.force.Y != 0.0f)
                {
                    float direct = _state.force.Y;
                    _state.force.Y -= (float)elapsed * _gravity;
                    if (direct > 0.0f)
                    {
                        if (_state.force.Y <= 0.0f)
                        {
                            _state.force.Y = 0.0f;
                        }
                    }
                    else
                    {
                        if (_state.force.Y >= 0.0f)
                        {
                            _state.force.Y = 0.0f;
                        }
                    }
                }
                 _state.effectTime = 0.0f;
            }
            _state.effectTime += (float)elapsed;
            float mobility = _state.effectTime * _gravity * GravityEmulationAdjust;
            element.AddPositionY(-mobility);

            _state.previousPosition = currentPosition;
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