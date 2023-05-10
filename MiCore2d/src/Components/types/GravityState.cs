using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// GravityState
    /// </summary>
    public struct GravityState
    {
        public float effectTime;
        public Vector3 force;
        public float gravityMobility;

        public GravityState(float _effectTime, Vector3 _force, float _mobility)
        {
            effectTime = _effectTime;
            force = _force;
            gravityMobility = _mobility;
        }
    }
}