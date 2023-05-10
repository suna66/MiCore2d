using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// GravityState
    /// </summary>
    public struct GravityState
    {
        public float fallingTime;
        public float inertiaTime;
        public Vector3 force;
        public float gravityMobility;

        public GravityState(float _fallingTime, float _inertiaTime, Vector3 _force, float _mobility)
        {
            fallingTime = _fallingTime;
            inertiaTime = _inertiaTime;
            force = _force;
            gravityMobility = _mobility;
        }
    }
}