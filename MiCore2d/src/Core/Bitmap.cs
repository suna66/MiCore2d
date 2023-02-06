using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// Bitmap.
    /// </summary>
    public class Bitmap
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        private Bitmap()
        {
        }

        /// <summary>
        /// CreateArrayBitmap. Create one bitmpa data from multiple data.
        /// </summary>
        /// <param name="files">image file list</param>
        /// <param name="width">widht of an image file</param>
        /// <param name="height">height of an image file</param>
        /// <returns>concated image data</returns>
        public static SKBitmap CreateArrayBitmap(string[] files, out int width, out int height)
        {
            SKBitmap bmp;
            SKCanvas canvas;

            if (files == null || files.Length == 0)
            {
                throw new ArgumentException("parameter is null or zero");
            }

            int _width;
            int _height;
            //check texture size
            SKBitmap image = SKBitmap.Decode(files[0]);
            _width = image.Width;
            _height = image.Height;

            width = _width;
            height = _height;

            bmp = new SKBitmap(width, height * files.Length, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas = new SKCanvas(bmp);
            drawArrayBitmap(canvas, files, _width, _height);
            canvas.Dispose();

            return bmp;
        }

        /// <summary>
        /// drawArrayBitmap. draw image data to SKCanvos.
        /// </summary>
        /// <param name="canvas">SKCanvas</param>
        /// <param name="files">image files</param>
        /// <param name="width">width of an image file</param>
        /// <param name="height">height of an image file</param>
        private static void drawArrayBitmap(SKCanvas canvas, string[] files, int width, int height)
        {
            for (int i = 0; i < files.Length; i++)
            {
                SKBitmap image = SKBitmap.Decode(files[i]);
                canvas.DrawBitmap(image, 0, height * i);
                image.Dispose();
            }
            canvas.Flush();
        }

        /// <summary>
        /// CreateBitmap. create bitmap data.
        /// </summary>
        /// <param name="width">image width</param>
        /// <param name="height">image height</param>
        /// <param name="r">red</param>
        /// <param name="g">blue</param>
        /// <param name="b">green</param>
        /// <returns>image data</returns>
        public static SKBitmap CreateBitmap(int width, int height, byte r, byte g, byte b)
        {
            SKBitmap bmp;
            SKCanvas canvas;

            bmp = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Opaque);
            canvas = new SKCanvas(bmp);
            SKPaint paint = new SKPaint();
            paint.Color = new SKColor(r, g, b);

            canvas.DrawRect(0, 0, width, height, paint);

            canvas.Flush();
            paint.Dispose();
            canvas.Dispose();

            return bmp;
        }
    }
}
