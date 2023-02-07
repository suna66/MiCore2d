using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// Texture2dTile.
    /// </summary>
    public class Texture2dTile : Texture
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="path">file paht</param>
        /// <param name="tileW">tile width</param>
        /// <param name="tileH">tile height</param>
        /// <returns>instance</returns>
        public Texture2dTile(string path, int tileW, int tileH) : base(TextureTarget.Texture2DArray)
        {
            if (path == null)
            {
                throw new ArgumentException("parameter is null");
            }
            GenHandle(TextureTarget.Texture2DArray);
            int texCount = 0;
            using(Stream stream = File.OpenRead(path))
            {
                texCount = loadTexture(stream, tileW, tileH);
            }
            Width = tileW;
            Height = tileH;
            SetTexParameter();
            UnBind();

            textureCount = texCount;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="tileW">tile width</param>
        /// <param name="tileH">tile height</param>
        /// <returns>instance</returns>
        public Texture2dTile(Stream stream, int tileW, int tileH) : base(TextureTarget.Texture2DArray)
        {
            if (stream == null)
            {
                throw new ArgumentException("parameter is null");
            }
            GenHandle(TextureTarget.Texture2DArray);
            int texCount = loadTexture(stream, tileW, tileH);

            Width = tileW;
            Height = tileH;
            SetTexParameter();
            UnBind();

            textureCount = texCount;
        }

        /// <summary>
        /// loadTexture.
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="tileW">tile width</param>
        /// <param name="tileH">tile height</param>
        /// <returns>texture count</returns>
        private int loadTexture(Stream stream, int tileW, int tileH)
        {
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            int tilesX = (int)(image.Width / tileW);
            int tilesY = (int)(image.Height / tileH);
            int texCount = tilesX * tilesY;

            int tilesSizeX = tileW * 4;
            int rowLength = tilesX * tilesSizeX;

            GL.TexImage3D(TextureTarget.Texture2DArray,
                0,
                PixelInternalFormat.Rgba,
                tileW, tileH,
                texCount,
                0,
                PixelFormat.Rgba, PixelType.UnsignedByte,
                (IntPtr)0);
            
            byte[] imageData = image.Data;
            byte[] data = new byte[tilesSizeX * tileH];
            int index = 0;
            for (int j = 0; j < tilesY; j++)
            {
                for (int i = 0; i < tilesX; i++)
                {
                    int pos = (j * tileH) * rowLength + i * tilesSizeX;
                    for (int row = 0; row < tileH; row++)
                    {
                        System.Buffer.BlockCopy(imageData, pos + row * rowLength, data, row * tilesSizeX, tilesSizeX);
                    }
                    GL.TexSubImage3D(TextureTarget.Texture2DArray,
                        0, 0, 0,
                        index,
                        tileW, tileH,
                        1,
                        PixelFormat.Rgba,
                        PixelType.UnsignedByte,
                        data);
                    index++;
                }
            }
            return texCount;
        }
    }
}