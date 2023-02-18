using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MiCore2d
{
    /// <summary>
    /// LineSprite.
    /// </summary>
    public class LineSprite : Element
    {
        private Vector3 _endPosition = Vector3.Zero;

        private Color4 _color = Color4.Black;

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
            _color = new Color4(r, g, b, 1.0f);
        }

        /// <summary>
        /// SetColor.
        /// </summary>
        /// <param name="color">Color4 struct</param>
        public void SetColor(Color4 color)
        {
            _color = color;
        }

        /// <summary>
        /// GetColor.
        /// </summary>
        /// <returns>color</returns>
        public Vector3 GetColor()
        {
            return new Vector3(_color.R, _color.G, _color.B);
        }

        /// <summary>
        /// GetColor4
        /// </summary>
        /// <returns>Color4 struct</returns>
        public Color4 GetColor4()
        {
            return _color;
        }
    }
}