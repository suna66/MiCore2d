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

        /// <summary>
        /// Vector4ToFloat
        /// </summary>
        /// <param name="v">Vector4 Array</param>
        /// <returns>float array</returns>
        public static float[] Vector4ToFloat(Vector4[] v)
        {
            int i = 0;
            float[] array = new float[v.Length * 4];
            foreach(Vector4 vec in v)
            {
                array[i++] = vec.X;
                array[i++] = vec.Y;
                array[i++] = vec.Z;
                array[i++] = vec.W;
            }
            return array;
        }

        /// <summary>
        /// FloatToVector4
        /// </summary>
        /// <param name="array">float array</param>
        /// <returns>vector4 array</returns>
        public static Vector4[] FloatToVector4(float[] array)
        {
            int j = 0;
            Vector4[] vecArray = new Vector4[array.Length/4];
            for (int i = 0; i < array.Length; i += 4)
            {
                vecArray[j++] = new Vector4(array[i], array[i+1], array[i+2], array[i+3]);
            }
            return vecArray;
        }

        /// <summary>
        /// Vector4Zero
        /// </summary>
        /// <param name="num">array length</param>
        /// <returns>Vecor4 array</returns>
        public static Vector4[] Vector4Zero(int num)
        {
            Vector4[] vecArray = new Vector4[num];

            for (int i = 0; i < num; i++)
            {
                vecArray[i] = Vector4.Zero;
            }

            return vecArray;
        }
    }
}