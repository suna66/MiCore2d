#pragma warning disable CS8765
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Line.
    /// </summary>
    public struct Line : IEquatable<Line>
    {
        private Vector2 _point1 = Vector2.Zero;
        private Vector2 _point2 = Vector2.Zero;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value1">position 1</param>
        /// <param name="value2">position 2</param>
        public Line(float value1, float value2)
        {
            _point1 = new Vector2(value1);
            _point2 = new Vector2(value2);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="x1">position x1</param>
        /// <param name="y1">position y1</param>
        /// <param name="x2">position x2</param>
        /// <param name="y2">position y2</param>
        public Line(float x1, float y1, float x2, float y2)
        {
            _point1 = new Vector2(x1, y1);
            _point2 = new Vector2(x2, y2);  
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value1">position 1</param>
        /// <param name="value2">position 2</param>
        public Line(Vector2 value1, Vector2 value2)
        {
            _point1 = value1;
            _point2 = value2;  
        }

        /// <summary>
        /// Point1
        /// </summary>
        /// <value>positoin 1</value>
        public Vector2 Point1 { get => _point1; }

        /// <summary>
        /// Point2
        /// </summary>
        /// <value>position 2</value>
        public Vector2 Point2 { get => _point2; }

        /// <summary>
        /// operation ==
        /// </summary>
        /// <param name="left">line value 1</param>
        /// <param name="right">line value 2</param>
        /// <returns>true: same value</returns>
        public static bool operator == (Line left, Line right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// operation !=
        /// </summary>
        /// <param name="left">line value 1</param>
        /// <param name="right">line value 2</param>
        /// <returns>true: not same value</returns>
        public static bool operator != (Line left, Line right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">target object</param>
        /// <returns>true: same value</returns>
        public override bool Equals(object obj)
        {
            return obj is Line && Equals((Line)obj);
        }

        /// <summary>
        /// Equals.
        /// </summary>
        /// <param name="other">target line</param>
        /// <returns>true: same line</returns>
        public bool Equals(Line other)
        {
            return _point1.X == other.Point1.X && _point1.Y == other.Point1.Y && _point2.X == other.Point2.X && _point2.Y == other.Point2.Y;
        }

        /// <summary>
        /// GetHasCode.
        /// </summary>
        /// <returns>code</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_point1.X, _point1.Y);
        }
    }
}