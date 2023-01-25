#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class MultiSprite : Element
    {
        private float[]? _positionMap = null;

        public MultiSprite(Texture tex, float unitSize, string rendererName) : base()
        {
            texture = tex;

            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            unit = unitSize;
            RendererName = rendererName;
        }

        public void SetPositionMap(float[] map)
        {
            _positionMap = map;
        }

        public float[] GetPosisionMap()
        {
            return _positionMap;
        }
    }
}