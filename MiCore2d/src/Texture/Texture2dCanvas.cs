using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// Texture2d
    /// </summary>
    public class Texture2dCanvas : Texture
    {
        private bool _disposed = false;
        private SKBitmap bmp;
        private SKCanvas gfx;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">texture path</param>
        /// <returns>instance</returns>
        public Texture2dCanvas(int width, int height) : base(TextureTarget.Texture2D)
        {
            Width = width;
            Height = height;

            bmp = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Opaque);
            gfx = new SKCanvas(bmp);

            loadTexture();
        }

        /// <summary>
        /// GetCanvas
        /// </summary>
        /// <returns>SKCanvas</returns>
        public SKCanvas GetCanvas()
        {
            return gfx;
        }

        /// <summary>
        /// GetBitmap
        /// </summary>
        /// <returns>SKBitmap</returns>
        public SKBitmap GetBitmap()
        {
            return bmp;
        }

        /// <summary>
        /// Flush canvas data.
        /// </summary>
        public void Flush()
        {
            gfx.Flush();
            IntPtr pixels = bmp.GetPixels();
            if (pixels == IntPtr.Zero)
                return;
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0,
              0, 0, bmp.Width, bmp.Height,
              PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
        }

        /// <summary>
        /// loadTexture.
        /// </summary>
        /// <param name="stream">stream data</param>
        private void loadTexture()
        {
            GenHandle();
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Rgba,
                Width, Height,
                0,
                PixelFormat.Rgba, PixelType.UnsignedByte,
                IntPtr.Zero
            );
            SetTexParameter();
            UnBind();
            textureCount = 1;
        }


        /// <summary>
        /// Dispose
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose(true);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    gfx.Dispose();
                    bmp.Dispose();
                }
                _disposed = true;
            }
        }

        ~Texture2dCanvas()
        {
            Dispose(false);
        }
    }
}