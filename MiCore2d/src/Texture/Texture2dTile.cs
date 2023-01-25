using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    public class Texture2dTile : Texture
    {
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
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                int tilesX = (int)(image.Width / tileW);
                int tilesY = (int)(image.Height / tileH);
                texCount = tilesX * tilesY;

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
            }
            Width = tileW;
            Height = tileH;
            SetTexParameter();
            UnBind();

            textureCount = texCount;
        }
    }
}