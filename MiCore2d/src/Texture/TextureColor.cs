using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// TextureColor.
    /// </summary>
    public class TextureColor : Texture
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
        /// <returns>instance</returns>
        public TextureColor(byte r, byte g, byte b) : base(TextureTarget.Texture2D)
        {
            GenHandle();

            using(SKBitmap bitmap = Bitmap.CreateBitmap(32, 32, r, g, b))
            {
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    32, 32,
                    0,
                    PixelFormat.Rgba, PixelType.UnsignedByte,
                    bitmap.GetPixels()
                );
            }
            Width = 32;
            Height = 32;
            SetTexParameter();
            UnBind();
            textureCount = 1;
        }
    }
}