#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// TilemapSprite.
    /// </summary>
    public class TilemapSprite : Element
    {
        private float[]? _positionMap = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tex">texture</param>
        /// <param name="unitSize">unit size</param>
        /// <param name="renderer">renderer</param>
        /// <returns>TimemapSprite</returns>
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

        /// <summary>
        /// SetPositionMap.
        /// </summary>
        /// <param name="map">position map list</param>
        public void SetPositionMap(float[] map)
        {
            _positionMap = map;
        }

        /// <summary>
        /// GetPositionMap.
        /// </summary>
        /// <returns>position map list</returns>
        public float[] GetPositionMap()
        {
            return _positionMap;
        }
    }
}