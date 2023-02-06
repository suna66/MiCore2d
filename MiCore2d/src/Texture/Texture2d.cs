using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// Texture2d
    /// </summary>
    public class Texture2d : Texture
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">texture path</param>
        /// <returns>instance</returns>
        public Texture2d(string path) : base(TextureTarget.Texture2D)
        {
            if (path == null)
            {
                throw new ArgumentException("parameter is null");
            }
            using(Stream stream = File.OpenRead(path))
            {
                loadTexture(stream);
            }
        }

        /// <summary>
        /// loadTexture.
        /// </summary>
        /// <param name="stream">stream data</param>
        private void loadTexture(Stream stream)
        {
            GenHandle();
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
            Width = image.Width;
            Height = image.Height;
            SetTexParameter();
            UnBind();
            textureCount = 1;
        }
    }
}