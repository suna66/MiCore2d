#pragma warning disable CS8765
using OpenTK.Mathematics;

namespace MiCore2d
{
    public struct Line : IEquatable<Line>
    {
        private Vector2 _point1 = Vector2.Zero;
        private Vector2 _point2 = Vector2.Zero;

        public Line(float value1, float value2)
        {
            _point1 = new Vector2(value1);
            _point2 = new Vector2(value2);
        }

        public Line(float x1, float y1, float x2, float y2)
        {
            _point1 = new Vector2(x1, y1);
            _point2 = new Vector2(x2, y2);  
        }

        public Line(Vector2 value1, Vector2 value2)
        {
            _point1 = value1;
            _point2 = value2;  
        }


        public Vector2 Point1 { get => _point1; }

        public Vector2 Point2 { get => _point2; }

        public bool LineCollision(Line other)
        {
            float uA = ((other.Point2.X - other.Point1.X) * (_point1.Y - other.Point1.Y) - (other.Point2.Y - other.Point1.Y) * (_point1.X - other.Point1.X)) 
                    / ((other.Point2.Y - other.Point1.Y) * (_point2.X - _point1.X) - (other.Point2.X - other.Point1.X) * (_point2.Y - _point1.Y));
            float uB = ((_point2.X - _point1.X) * (_point1.Y - other.Point1.Y) - (_point2.Y - _point1.Y) * (_point1.X - other.Point1.X))
                    / ((other.Point2.Y - other.Point1.Y) * (_point2.X - _point1.X) - (other.Point2.X - other.Point1.X) * (_point2.Y - _point1.Y));
            if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
            {
                return true;
            }
            return false;
        }

        public Vector2 IntersectionPoint(Line other)
        {
            Vector2 point = Vector2.Zero;
            float uA = ((other.Point2.X - other.Point1.X) * (_point1.Y - other.Point1.Y) - (other.Point2.Y - other.Point1.Y) * (_point1.X - other.Point1.X)) 
                    / ((other.Point2.Y - other.Point1.Y) * (_point2.X - _point1.X) - (other.Point2.X - other.Point1.X) * (_point2.Y - _point1.Y));
            float uB = ((_point2.X - _point1.X) * (_point1.Y - other.Point1.Y) - (_point2.Y - _point1.Y) * (_point1.X - other.Point1.X))
                    / ((other.Point2.Y - other.Point1.Y) * (_point2.X - _point1.X) - (other.Point2.X - other.Point1.X) * (_point2.Y - _point1.Y));
            if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
            {
                float intersectionX = _point1.X + (uA * (_point2.X - _point1.X));
                float intersectionY = _point1.Y + (uA * (_point2.Y - _point1.Y));
                point = new Vector2(intersectionX, intersectionY);
            }
            return point;
        }

        public static bool operator == (Line left, Line right)
        {
            return left.Equals(right);
        }

        public static bool operator != (Line left, Line right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return obj is Line && Equals((Line)obj);
        }

        public bool Equals(Line other)
        {
            return _point1.X == other.Point1.X && _point1.Y == other.Point1.Y && _point2.X == other.Point2.X && _point2.Y == other.Point2.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_point1.X, _point1.Y);
        }
    }
}