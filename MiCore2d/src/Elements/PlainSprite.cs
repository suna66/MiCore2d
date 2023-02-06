using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// PlainSprite.
    /// </summary>
    public class PlainSprite : Element
    {
        private Vector3 _color = Vector3.One;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="unitSize">unit size</param>
        /// <returns>PlainSprite</returns>
        public PlainSprite(float unitSize) : base()
        {
            texture = null;

            scale.X = unitSize;
            scale.Y = unitSize;
            unit = unitSize;
            DrawRenderer = RendererManager.GetInstance().GetRenderer<PolygonRenderer>();
        }

        /// <summary>
        /// SetColor.
        /// </summary>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
        public void SetColor(float r, float g, float b)
        {
            _color = new Vector3(r, g, b);
        }

        /// <summary>
        /// GetColor
        /// </summary>
        /// <returns>color</returns>
        public Vector3 GetColor()
        {
            return _color;
        }
    }
}