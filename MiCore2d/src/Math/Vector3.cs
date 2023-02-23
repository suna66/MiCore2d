using TK = OpenTK.Mathematics;

namespace MiCore2d.Math
{
    public struct  Vector3
    {
        private TK.Vector3 Vec;

        public static Vector3 Zero
        {
            get => new Vector3(0.0f);
        }

        public static Vector3 One
        {
            get => new Vector3(1.0f);
        }

        public static Vector3 UnitX
        {
            get => new Vector3(1.0f, 0.0f, 0.0f);
        }

        public static Vector3 UnitY
        {
            get => new Vector3(0.0f, 1.0f, 0.0f);
        }

        public static Vector3 UnitZ
        {
            get => new Vector3(0.0f, 0.0f, 1.0f);
        }

        public float X
        {
            get => Vec.X;
            set
            {
                Vec.X = value;
            }
        }

        public float Y
        {
            get => Vec.Y;
            set
            {
                Vec.Y = value;
            }
        }

        public float Z
        {
            get => Vec.Z;
            set
            {
                Vec.Z = value;
            }
        }

        Vector3(TK.Vector3 vec)
        {
            Vec = vec;
        }

        Vector3(Vector3 vec)
        {
            Vec = vec.GetOpenTKVector3();
        }

        Vector3(float x, float y, float z)
        {
            Vec = new TK.Vector3(x, y, z);
        }

        Vector3(float value)
        {
            Vec = new TK.Vector3(value, value, value);
        }

        Vector3(Vector2 vec)
        {
            Vec = new TK.Vector3(vec.X, vec.Y, 0.0f);
        }

        public TK.Vector3 GetOpenTKVector3()
        {
            return Vec;
        }

        public static Vector3 operator + (Vector3 left, Vector3 right)
        {
            return new Vector3(left.GetOpenTKVector3() + right.GetOpenTKVector3());
        }

        public static Vector3 operator - (Vector3 left, Vector3 right)
        {
            return new Vector3(left.GetOpenTKVector3() - right.GetOpenTKVector3());
        }

        public static Vector3 operator * (Vector3 left, Vector3 right)
        {
            return new Vector3(left.GetOpenTKVector3() * right.GetOpenTKVector3());
        }

        public static Vector3 operator / (Vector3 left, Vector3 right)
        {
            return new Vector3(left.GetOpenTKVector3() / right.GetOpenTKVector3());
        }
    }
}