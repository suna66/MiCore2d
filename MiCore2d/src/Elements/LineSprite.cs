using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// LineSprite.
    /// </summary>
    public class LineSprite : Element
    {
        private Vector3 _endPosition = Vector3.Zero;

        private Vector3 _color = Vector3.Zero;

        /// <summary>
        /// LineSprite.
        /// </summary>
        /// <returns>LineSprite</returns>
        public LineSprite() : base()
        {
            texture = null;
            unit = 1.0f;
            DrawRenderer = RendererManager.GetInstance().GetRenderer<LineRenderer>();
        }

        /// <summary>
        /// StartPosition.
        /// </summary>
        /// <value>line start position</value>
        public Vector3 StartPosition
        {
            get => position;
            set {
                position = value;
            }
        }

        /// <summary>
        /// EndPosition
        /// </summary>
        /// <value>line end position</value>
        public Vector3 EndPosition
        {
            get => _endPosition;
            set {
                _endPosition = value;
            }
        }

        /// <summary>
        /// SetLine.
        /// </summary>
        /// <param name="start">start position</param>
        /// <param name="end">end position</param>
        public void SetLine(Vector3 start, Vector3 end)
        {
            StartPosition = start;
            EndPosition = end;
        }

        /// <summary>
        /// SetColor
        /// </summary>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
        public void SetColor(float r, float g, float b)
        {
            _color = new Vector3(r, g, b);
        }

        /// <summary>
        /// GetColor.
        /// </summary>
        /// <returns>color</returns>
        public Vector3 GetColor()
        {
            return _color;
        }
    }
}