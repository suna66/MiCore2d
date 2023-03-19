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
        /// Constructor
        /// </summary>
        /// <param name="file">image file path</param>
        /// <param name="tileWidth">size of tile width</param>
        /// <param name="tileHeight">size of tile height</param>
        /// <param name="unitSize">unit size</param>
        /// <param name="renderer">renderer</param>
        /// <returns></returns>
        public TilemapSprite(string file, int tileWidth, int tileHeight, float unitSize, InstancedRenderer renderer) : base()
        {
            texture = new Texture2dTile(file, tileWidth, tileHeight);
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