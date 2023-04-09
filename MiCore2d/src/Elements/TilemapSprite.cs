#nullable disable warnings
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// TilemapSprite.
    /// </summary>
    public class TilemapSprite : Element
    {
        private bool disposed = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">image file path</param>
        /// <param name="tileWidth">size of tile width</param>
        /// <param name="tileHeight">size of tile height</param>
        /// <param name="unitSize">unit size</param>
        /// <param name="tileMap">tilemap data</param>
        /// <param name="isDynamic">dynamic renderer of tile</param>
        /// <returns></returns>
        public TilemapSprite(string file, int tileWidth, int tileHeight, float unitSize,  float[] tileMap, bool isDynamic) : base()
        {
            texture = new Texture2dTile(file, tileWidth, tileHeight);
            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            Unit = unitSize;
            DrawRenderer = new TilemapRenderer(tileMap, isDynamic);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file">image file path</param>
        /// <param name="tileWidth">size of tile width</param>
        /// <param name="tileHeight">size of tile height</param>
        /// <param name="unitSize">unit size</param>
        /// <param name="tileNum">number of tiles</param>
        /// <param name="isDynamic">dynamic renderer of tile</param>
        /// <returns></returns>
        public TilemapSprite(string file, int tileWidth, int tileHeight, float unitSize, int tileNum, bool isDynamic) : base()
        {
            texture = new Texture2dTile(file, tileWidth, tileHeight);
            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            Unit = unitSize;
            DrawRenderer = new TilemapRenderer(tileNum, isDynamic);   
        }

        /// <summary>
        /// SetTileMap.
        /// </summary>
        /// <param name="map">position map list</param>
        public void SetTileMap(float[] map)
        {
            TilemapRenderer? tilemapRenderer = DrawRenderer as TilemapRenderer;
            tilemapRenderer?.SetTileMap(map);
        }

        /// <summary>
        /// GetPositionMap.
        /// </summary>
        /// <returns>position map list</returns>
        public float[] GetTileMap()
        {
            TilemapRenderer? tilemapRenderer = DrawRenderer as TilemapRenderer;
            return tilemapRenderer?.GetTileMap();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            if (!disposed)
            {
                //DrawRenderer?.Dispose();
                base.Dispose();
                disposed = true;
            }
        }
    }
}