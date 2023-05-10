#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Gravity. emulate gravity components.
    /// </summary>
    public class Gravity : Component
    {
        /// <summary>
        /// _gravity.
        /// </summary>
        private float _gravity = 1.0f;

        /// <summary>
        /// effect state.
        /// </summary>
        private GravityState _state;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Gravity()
        {
            _state = new GravityState(0.0f, 0.0f, Vector3.Zero, 0.0f);
        }

        /// <summary>
        /// GravityElulationAdjust
        /// </summary>
        /// <value>float</value>
        public float GravityEmulationAdjust {get; set;} = 0.016f;

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
            if (_state.force.X != 0.0f || _state.force.Y != 0.0f)
            {
                element.AddPosition(_state.force * GravityEmulationAdjust);
                _state.inertiaTime += (float)elapsed;
                if (_state.force.X != 0.0f)
                {
                    float direct = _state.force.X;
                    _state.force.X -= _state.inertiaTime * GravityEmulationAdjust * _gravity;
                    if (direct > 0.0f)
                    {
                        if (_state.force.X <= 0.0f)
                        {
                            _state.force.X = 0.0f;
                            _state.inertiaTime = 0.0f;
                        }
                    }
                    else
                    {
                        if (_state.force.X >= 0.0f)
                        {
                            _state.force.X = 0.0f;
                            _state.inertiaTime = 0.0f;
                        }
                    }
           
                }
                if (_state.force.Y != 0.0f)
                {
                    float direct = _state.force.Y;
                    _state.force.Y -= _state.inertiaTime * GravityEmulationAdjust * _gravity;
                    if (direct > 0.0f)
                    {
                        if (_state.force.Y <= 0.0f)
                        {
                            _state.force.Y = 0.0f;
                            _state.inertiaTime = 0.0f;
                        }
                    }
                    else
                    {
                        if (_state.force.Y >= 0.0f)
                        {
                            _state.force.Y = 0.0f;
                            _state.inertiaTime = 0.0f;
                        }
                    }
                }
                _state.fallingTime = 0.0f;
            }
            float mobility = _state.fallingTime * _gravity * GravityEmulationAdjust;
            element.AddPositionY(-mobility);
            _state.fallingTime += (float)elapsed;

            _state.gravityMobility = mobility;
        }

        /// <summary>
        /// OnEnterCollision
        /// </summary>
        /// <param name="collisionInfo"></param>
        public override void OnEnterCollision(CollisionInfo collisionInfo)
        {
            _state.fallingTime = 0.0f;
            _state.inertiaTime = 0.0f;
            _state.force = Vector3.Zero;
        }

        /// <summary>
        /// OnStayCollision
        /// </summary>
        /// <param name="collisionInfo"></param>
        public override void OnStayCollision(CollisionInfo collisionInfo)
        {
            _state.fallingTime = 0.0f;
            _state.inertiaTime = 0.0f;
            _state.force = Vector3.Zero;
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