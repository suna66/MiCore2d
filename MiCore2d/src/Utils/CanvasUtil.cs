using SkiaSharp;

namespace MiCore2d
{
    public class CanvasUtil
    {
        /// <summary>
        /// Constructor
        /// </summary>
        private CanvasUtil()
        {
        }

        /// <summary>
        /// Clear.
        /// </summary>
        public static void Clear(SKCanvas gfx, SKColor color)
        {
            gfx.Clear(color);
        }

        /// <summary>
        /// Color
        /// </summary>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
        /// <param name="a">alpha</param>
        /// <returns>SKColor</returns>
        public static SKColor Color(byte r, byte g, byte b, byte a)
        {
            return new SKColor(r, g, b, a);
        }


        /// <summary>
        /// GetFont
        /// </summary>
        /// <param name="resourceName">resource name of font</param>
        /// <returns>SKTypeface</returns>
        public static SKTypeface Font(string resourceName)
        {
            SKTypeface font;
            using(Stream stream = Resources.ReadStream(resourceName))
            {
                font  = SKTypeface.FromStream(stream);
            }
            return font;
        }


        /// <summary>
        /// DrawString
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="font"></param>
        public static void DrawString(SKCanvas gfx, int x, int y, string text, SKColor color, int size, SKTypeface font = null!)
        {
            SKPaint paint = new SKPaint();
            paint.Color = color;
            paint.TextSize = size;
            if (font != null)
            {
                paint.Typeface = font;
            }
            gfx.DrawText(text, x, y + paint.TextSize, paint);
        }

        /// <summary>
        /// DrawRect
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="style"></param>
        public static void DrawRect(SKCanvas gfx, float x, float y, float w, float h, SKColor color, int size, SKPaintStyle style)
        {
            SKPaint paint = new SKPaint();
            paint.Color = color;
            paint.Style = style;
            paint.StrokeWidth = size;
            gfx.DrawRect(x, y, w, h, paint);
        }

        /// <summary>
        /// DrawLine
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="style"></param>
        public static void DrawLine(SKCanvas gfx, float x0, float y0, float x1, float y1, SKColor color, int size, SKPaintStyle style)
        {
            SKPaint paint = new SKPaint();
            paint.Color = color;
            paint.Style = style;
            paint.StrokeWidth = size;
            gfx.DrawLine(x0, y0, x1, y1, paint);
        }

        /// <summary>
        /// DrawOval
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="rx"></param>
        /// <param name="ry"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="style"></param>
        public static void DrawOval(SKCanvas gfx, float cx, float cy, float rx, float ry, SKColor color, int size, SKPaintStyle style)
        {
            SKPaint paint = new SKPaint();
            paint.Color = color;
            paint.Style = style;
            paint.StrokeWidth = size;
            gfx.DrawOval(cx, cy, rx, ry, paint);
        }

        /// <summary>
        /// DrawPoint
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="style"></param>
        public static void DrawPoint(SKCanvas gfx, float x, float y, SKColor color, int size, SKPaintStyle style)
        {
            SKPaint paint = new SKPaint();
            paint.Color = color;
            paint.Style = style;
            paint.StrokeWidth = size;
            gfx.DrawPoint(x, y, paint);
        }
    }
}