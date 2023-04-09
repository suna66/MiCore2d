#nullable disable warnings
using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// CanvasSprite.
    /// </summary>
    public class CanvasSprite : Element
    {
        private bool disposed = false;

        /// <summary>
        /// Constractor.
        /// </summary>
        /// <param name="file">Image file path</param>
        /// <param name="unitSize">unit size</param>
        /// <returns></returns>
        public CanvasSprite(int width, int height, float unitSize) : base()
        {
            texture = new Texture2dCanvas(width, height);
            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            Unit = unitSize;
            DrawRenderer = new CanvasRenderer();
        }

        /// <summary>
        /// GetCanvas
        /// </summary>
        /// <returns>SKCanvas</returns>
        public SKCanvas GetCanvas()
        {
            if (texture == null) return null!;
            Texture2dCanvas? texCanvas = texture as Texture2dCanvas;
            return texCanvas?.GetCanvas()!;
        }

        /// <summary>
        /// GetBitmap
        /// </summary>
        /// <returns>SKBitmao</returns>
        public SKBitmap GetBitmap()
        {
            if (texture == null) return null!;
            Texture2dCanvas? texCanvas = texture as Texture2dCanvas;
            return texCanvas?.GetBitmap()!;
        }

        /// <summary>
        /// Flush
        /// </summary>
        public void Flush()
        {
            if (texture == null) return;
            Texture2dCanvas? texCanvas = texture as Texture2dCanvas;
            texCanvas?.Flush();
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