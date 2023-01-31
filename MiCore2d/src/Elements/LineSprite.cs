using OpenTK.Mathematics;

namespace MiCore2d
{
    public class LineSprite : Element
    {
        private Vector3 _endPosition = Vector3.Zero;

        private Vector3 _color = Vector3.Zero;

        public LineSprite() : base()
        {
            texture = null;
            unit = 1.0f;
        }

        public Vector3 StartPosition
        {
            get => position;
            set {
                position = value;
            }
        }

        public Vector3 EndPosition
        {
            get => _endPosition;
            set {
                _endPosition = value;
            }
        }

        public void SetLine(Vector3 start, Vector3 end)
        {
            StartPosition = start;
            EndPosition = end;
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