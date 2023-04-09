using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// ImageSprite.
    /// </summary>
    public class ImageSprite : Element
    {
        private bool disposed = false;

        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="file">Image file path</param>
        /// <param name="unitSize">unit size</param>
        /// <returns></returns>
        public ImageSprite(string file, float unitSize) : base()
        {
            texture = new Texture2d(file);
            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            Unit = unitSize;
            DrawRenderer = new TextureRenderer();
        }

        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="files">list of image file path</param>
        /// <param name="width">width of an image file</param>
        /// <param name="height">heght of an image file</param>
        /// <param name="unitSize">unit size</param>
        /// <returns></returns>
        public ImageSprite(string[] files, int width, int height, float unitSize): base()
        {
            texture = new Texture2dArray(files, width, height);
            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            Unit = unitSize;
            DrawRenderer = new TextureArrayRenderer();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="file">image file path</param>
        /// <param name="tileWidth">size of an tile width</param>
        /// <param name="tileHeight">size of an tile height</param>
        /// <param name="unitSize">unit size</param>
        /// <returns></returns>
        public ImageSprite(string file, int tileWidth, int tileHeight, float unitSize) : base()
        {
            texture = new Texture2dTile(file, tileWidth, tileHeight);
            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            Unit = unitSize;
            DrawRenderer = new TextureArrayRenderer();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            if (!disposed)
            {
                base.Dispose();
                disposed = true;
            }
        }
    }
}