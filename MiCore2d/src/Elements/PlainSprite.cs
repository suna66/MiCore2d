using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MiCore2d
{
    /// <summary>
    /// PlainSprite.
    /// </summary>
    public class PlainSprite : Element
    {
        private Color4 _color = Color4.Black;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="unitSize">unit size</param>
        /// <returns>PlainSprite</returns>
        public PlainSprite(float unitSize) : base()
        {
            texture = null;

            // scale.X = unitSize;
            // scale.Y = unitSize;
            Unit = unitSize;
            DrawRenderer = new PolygonRenderer(unitSize, 1.0f);
        }

        /// <summary>
        /// SetColor.
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
        /// GetColor
        /// </summary>
        /// <returns>color</returns>
        public Vector3 GetColor()
        {
            return new Vector3(_color.R, _color.G, _color.B);
        }

        /// <summary>
        /// GetColor4.
        /// </summary>
        /// <returns>Color4</returns>
        public Color4 GetColor4()
        {
            return _color;
        }
    }
}