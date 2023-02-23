using TK = OpenTK.Mathematics;

namespace MiCore2d.Math
{
    public struct  Vector2
    {
        private TK.Vector2 Vec;

        public static Vector2 Zero
        {
            get => new Vector2(0.0f);
        }

        public static Vector2 One
        {
            get => new Vector2(1.0f);
        }

        public static Vector2 UnitX
        {
            get => new Vector2(1.0f, 0.0f);
        }

        public static Vector2 UnitY
        {
            get => new Vector2(0.0f, 1.0f);
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


        Vector2(TK.Vector2 vec)
        {
            Vec = vec;
        }

        Vector2(Vector2 vec)
        {
            Vec = vec.GetOpenTKVector2();
        }

        Vector2(float x, float y)
        {
            Vec = new TK.Vector2(x, y);
        }

        Vector2(float value)
        {
            Vec = new TK.Vector2(value, value);
        }

        Vector2(Vector3 vec)
        {
            Vec = new TK.Vector2(vec.X, vec.Y);
        }

        public TK.Vector2 GetOpenTKVector2()
        {
            return Vec;
        }

        public static Vector2 operator + (Vector2 left, Vector2 right)
        {
            return new Vector2(left.GetOpenTKVector2() + right.GetOpenTKVector2());
        }

        public static Vector2 operator - (Vector2 left, Vector2 right)
        {
            return new Vector2(left.GetOpenTKVector2() - right.GetOpenTKVector2());
        }

        public static Vector2 operator * (Vector2 left, Vector2 right)
        {
            return new Vector2(left.GetOpenTKVector2() * right.GetOpenTKVector2());
        }

        public static Vector2 operator / (Vector2 left, Vector2 right)
        {
            return new Vector2(left.GetOpenTKVector2() / right.GetOpenTKVector2());
        }
    }
}