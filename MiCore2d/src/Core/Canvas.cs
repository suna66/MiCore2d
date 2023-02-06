using SkiaSharp;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Canvas.
    /// </summary>
    public class Canvas : IDisposable
    {
        private SKBitmap bmp;
        private SKCanvas gfx;
        private SKTypeface font;
        private SKPaint paint;

        private float[] _vertices = null!;

        private readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private int _elementBufferObject;
        private int _vertexBufferObject;
        private int _vertexArrayObject;

        private Shader _shader = null!;

        private Camera _camera = null!;

        private int texture;

        private bool _disposed = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="width">canvas width</param>
        /// <param name="height">canvas height</param>
        public Canvas(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height ");

            float aspectRatio = width/ (float)height;
            _camera = new Camera(Vector3.UnitZ, aspectRatio);
            _camera.CameraType = CAMERA_TYPE.ORTHONGRAPHIC;
            
            _vertices = new float[20]
            {
                 1.0f * aspectRatio,  1.0f, 0.0f, 1.0f, 0.0f,
                 1.0f * aspectRatio, -1.0f, 0.0f, 1.0f, 1.0f,
                -1.0f * aspectRatio, -1.0f, 0.0f, 0.0f, 1.0f,
                -1.0f * aspectRatio,  1.0f, 0.0f, 0.0f, 0.0f
            };

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            _elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            //_shader = new Shader("resources/basic.vert", "resources/basic.frag");
            _shader = new Shader(Resources.ReadText("MiCore2d.resources.basic.vert"), Resources.ReadText("MiCore2d.resources.basic.frag"));
            _shader.Use();

            int vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            int texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));



            bmp = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Opaque);
            gfx = new SKCanvas(bmp);
            //default setting for painting.
            font = SKTypeface.FromFamilyName("Serif");
            //font = SKTypeface.FromFile("font/ZenOldMincho-Medium.ttf");
            using(Stream stream = Resources.ReadStream("MiCore2d.resources.NotoSansJP-Thin.otf"))
            {
                font  = SKTypeface.FromStream(stream);
            }
            paint = new SKPaint();
            paint.Color = new SKColor(0, 0, 0);
            paint.Typeface = font;
            paint.TextSize = 12;
            paint.Style = SKPaintStyle.Fill;
            paint.StrokeWidth = 1;

            texture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexImage2D(TextureTarget.Texture2D,
              0,
              PixelInternalFormat.Rgba,
              width, height,
              0,
              PixelFormat.Rgba,
              PixelType.UnsignedByte,
              IntPtr.Zero);
        }

        /// <summary>
        /// SetColor. Setting paing color.
        /// </summary>
        /// <param name="r">red</param>
        /// <param name="g">green</param>
        /// <param name="b">blue</param>
        public void SetColor(byte r, byte g, byte b)
        {
            paint.Color = new SKColor(r, g, b);
        }

        /// <summary>
        /// SetFontSize. Setting font size.
        /// </summary>
        /// <param name="size">size</param>
        public void SetFontSize(int size)
        {
            paint.TextSize = size;
        }

        /// <summary>
        /// SetPaintStyle. Setting paint style.
        /// </summary>
        /// <param name="style">style</param>
        public void SetPaintStyle(SKPaintStyle style)
        {
            paint.Style = style;
        }

        /// <summary>
        /// SetStrokeWidth.
        /// </summary>
        /// <param name="width">width</param>
        public void SetStrokeWidth(int width)
        {
            paint.StrokeWidth = width;
        }

        /// <summary>
        /// Clear. Clear canvas.
        /// </summary>
        public void Clear()
        {
            gfx.Clear(new SKColor(0, 0, 0, 0));
        }

        /// <summary>
        /// DrawString. Drawing specified string.
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <param name="text">string</param>
        public void DrawString(int x, int y, string text)
        {
            gfx.DrawText(text, x, y + paint.TextSize, paint);
        }

        /// <summary>
        /// DrawRect. Drawing rectangle
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        public void DrawRect(float x, float y, float w, float h)
        {
            gfx.DrawRect(x, y, w, h, paint);
        }

        /// <summary>
        /// DrawLine. Drawing line.
        /// </summary>
        /// <param name="x0">start x</param>
        /// <param name="y0">start y</param>
        /// <param name="x1">end x</param>
        /// <param name="y1">end y</param>
        public void DrawLine(float x0, float y0, float x1, float y1)
        {
            gfx.DrawLine(x0, y0, x1, y1, paint);
        }

        /// <summary>
        /// DraawOval.
        /// </summary>
        /// <param name="cx">centor x</param>
        /// <param name="cy">centor y</param>
        /// <param name="rx">radius x</param>
        /// <param name="ry">radius y</param>
        public void DrawOval(float cx, float cy, float rx, float ry)
        {
            gfx.DrawOval(cx, cy, rx, ry, paint);
        }

        /// <summary>
        /// DrawPoint
        /// </summary>
        /// <param name="x">point x</param>
        /// <param name="y">point y</param>
        public void DrawPoint(float x, float y)
        {
            gfx.DrawPoint(x, y, paint);
        }

        /// <summary>
        /// Alpha. Setting alpha value.
        /// </summary>
        /// <value>value</value>
        public float Alpha { get; set; } = 1.0f;

        public void Update()
        {
            GL.BindVertexArray(_vertexArrayObject);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            _shader.Use();
            _shader.SetMatrix4("model", Matrix4.Identity);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
            _shader.SetFloat("texAlpha", 1.0f - Alpha);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Flush. flushing canvas.
        /// </summary>
        public void Flush()
        {
            gfx.Flush();
            IntPtr pixels = bmp.GetPixels();
            if (pixels == IntPtr.Zero)
                return;

            GL.BindVertexArray(_vertexArrayObject);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexSubImage2D(TextureTarget.Texture2D, 0,
              0, 0, bmp.Width, bmp.Height,
              PixelFormat.Rgba, PixelType.UnsignedByte, pixels);

            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disose.
        /// </summary>
        /// <param name="disposing">disposing managed object or not</param>
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    gfx.Dispose();
                    bmp.Dispose();
                }
                GL.DeleteBuffer(_vertexBufferObject);
                GL.DeleteBuffer(_elementBufferObject);
                GL.DeleteVertexArray(_vertexArrayObject);
                _disposed = true;
            }
        }
        
        /// <summary>
        /// Destructor.
        /// </summary>
        ~Canvas()
        {
            Dispose(false);
        }
    }
}
