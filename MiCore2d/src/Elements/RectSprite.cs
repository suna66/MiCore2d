using OpenTK.Mathematics;

namespace MiCore2d
{
    public class RectSprite : Element
    {
        private Vector3 _color = Vector3.One;

        public RectSprite(float unitSize) : base()
        {
            texture = null;

            scale.X = unitSize;
            scale.Y = unitSize;
            unit = unitSize;
            RendererName = "rect";
        }

        public void SetColor(float r, float g, float b)
        {
            _color = new Vector3(r, g, b);
        }

        public Vector3 GetColor()
        {
            return _color;
        }
    }
}