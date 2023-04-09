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
        /// TileMap
        /// </summary>
        /// <value></value>
        public Vector4[] TileMap { get; set; }

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
        public TilemapSprite(string file, int tileWidth, int tileHeight, float unitSize,  Vector4[] tileMap, bool isDynamic) : base()
        {
            texture = new Texture2dTile(file, tileWidth, tileHeight);
            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            Unit = unitSize;
            TileMap = tileMap;
            float[] tileMapF = VectorF.Vector4ToFloat(tileMap);
            DrawRenderer = new TilemapRenderer(tileMapF, isDynamic);
        }

        // /// <summary>
        // /// Constructor
        // /// </summary>
        // /// <param name="file">image file path</param>
        // /// <param name="tileWidth">size of tile width</param>
        // /// <param name="tileHeight">size of tile height</param>
        // /// <param name="unitSize">unit size</param>
        // /// <param name="tileMap">tilemap data</param>
        // /// <param name="isDynamic">dynamic renderer of tile</param>
        // /// <returns></returns>
        // public TilemapSprite(string file, int tileWidth, int tileHeight, float unitSize,  float[] tileMap, bool isDynamic) : base()
        // {
        //     texture = new Texture2dTile(file, tileWidth, tileHeight);
        //     float aspectRatio = texture.Width / (float)texture.Height;
        //     scale.X = unitSize * aspectRatio;
        //     scale.Y = unitSize;
        //     Unit = unitSize;
        //     TileMap = VectorF.FloatToVector4(tileMap);
        //     DrawRenderer = new TilemapRenderer(tileMap, isDynamic);
        // }

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
            TileMap = VectorF.Vector4Zero(tileNum);
            DrawRenderer = new TilemapRenderer(tileNum, isDynamic);   
        }

        // /// <summary>
        // /// SetTileMap.
        // /// </summary>
        // /// <param name="map">position map list</param>
        // public void UpdateTileMap(float[] map)
        // {
        //     TileMap = VectorF.FloatToVector4(map);

        //     TilemapRenderer? tilemapRenderer = DrawRenderer as TilemapRenderer;
        //     tilemapRenderer?.SetTileMap(map);
        // }

        /// <summary>
        /// UpdateTileMap
        /// </summary>
        /// <param name="map">Vector4 map data</param>
        public void UpdateTileMap(Vector4[] map)
        {
            TileMap = map;

            TilemapRenderer? tilemapRenderer = DrawRenderer as TilemapRenderer;
            tilemapRenderer?.SetTileMap(VectorF.Vector4ToFloat(map));
        }

        /// <summary>
        /// UpdateTileMap
        /// </summary>
        public void UpdateTileMap()
        {
            TilemapRenderer? tilemapRenderer = DrawRenderer as TilemapRenderer;
            tilemapRenderer?.SetTileMap(VectorF.Vector4ToFloat(TileMap));
        }

        /// <summary>
        /// GetTileMapArray
        /// </summary>
        /// <returns></returns>
        public float[] GetTileMapArray()
        {
            return VectorF.Vector4ToFloat(TileMap);
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