#pragma warning disable CS8765
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// VectorF Utility.
    /// </summary>
    public class VectorF
    {
        /// <summary>
        /// Vec3.
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="z">z position</param>
        /// <returns>vector3</returns>
        public static Vector3 Vec3(float x, float y, float z)
        {
            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Vec3.
        /// </summary>
        /// <param name="vec2">Vector2</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vec3(Vector2 vec2)
        {
            return new Vector3(vec2);
        }

        /// <summary>
        /// Vec3.
        /// </summary>
        /// <param name="v">float</param>
        /// <returns>Vector3</returns>
        public static Vector3 Vec3(float v)
        {
            return new Vector3(v);
        }

        /// <summary>
        /// Vec2.
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vec2(float x, float y)
        {
            return new Vector2(x, y);
        }

        /// <summary>
        /// Vec2.
        /// </summary>
        /// <param name="vec3">Vector3</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vec2(Vector3 vec3)
        {
            return new Vector2(vec3.X, vec3.Y);
        }

        /// <summary>
        /// Vec3.
        /// </summary>
        /// <param name="v">float</param>
        /// <returns>Vector2</returns>
        public static Vector2 Vec2(float v)
        {
            return new Vector2(v);
        }
    }
}