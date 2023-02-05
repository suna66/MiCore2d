#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class TilemapSprite : Element
    {
        private float[]? _positionMap = null;

        public TilemapSprite(Texture tex, float unitSize, InstancedRenderer renderer) : base()
        {
            texture = tex;

            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            unit = unitSize;
            DrawRenderer = renderer;
            _positionMap = renderer.GetTilemap();
        }

        public void SetPositionMap(float[] map)
        {
            _positionMap = map;
        }

        public float[] GetPositionMap()
        {
            return _positionMap;
        }
    }
}