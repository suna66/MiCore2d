using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    public class Texture2d : Texture
    {
        public Texture2d(string path) : base(TextureTarget.Texture2D)
        {
            if (path == null)
            {
                throw new ArgumentException("parameter is null");
            }
            int width = 0, height = 0;
            GenHandle();

            using(Stream stream = File.OpenRead(path))
            {
                ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    image.Width, image.Height,
                    0,
                    PixelFormat.Rgba, PixelType.UnsignedByte,
                    image.Data
                );
                width = image.Width;
                height = image.Height;
            }
            Width = width;
            Height = height;
            SetTexParameter();
            UnBind();
            textureCount = 1;
        }
    }
}