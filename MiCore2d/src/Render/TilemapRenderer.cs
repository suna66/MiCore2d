using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// TilemapRenderer.
    /// </summary>
    public class TilemapRenderer : Renderer
    {
        private float[] _tileMap;

        protected int tilemapBufferObject;

        private bool _disposed = false;

        private bool _isDynamic = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TilemapRenderer(float[] tileMap, bool isDynamic) : base(1.0f, 1.0f)
        {
            _tileMap = tileMap;
            _isDynamic = isDynamic;

            Init(Resources.ReadText("MiCore2d.resources.instanced.vert"), Resources.ReadText("MiCore2d.resources.instanced.frag"));
            InitTilemap();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public TilemapRenderer(float[] tileMap, bool isDynamic, float unitSize, float aspectRatio) : base(unitSize, aspectRatio)
        {
            _tileMap = tileMap;
            _isDynamic = isDynamic;

            Init(Resources.ReadText("MiCore2d.resources.instanced.vert"), Resources.ReadText("MiCore2d.resources.instanced.frag"));
            InitTilemap();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tileNum"></param>
        /// <param name="isDynamic"></param>
        /// <returns></returns>
        public TilemapRenderer(int tileNum, bool isDynamic) : base(1.0f, 1.0f)
        {
            _tileMap = Enumerable.Repeat<float>(0.0f, tileNum*4).ToArray();
            _isDynamic = isDynamic;
            
            Init(Resources.ReadText("MiCore2d.resources.instanced.vert"), Resources.ReadText("MiCore2d.resources.instanced.frag"));
            InitTilemap();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tileNum"></param>
        /// <param name="isDynamic"></param>
        /// <param name="unitSize"></param>
        /// <param name="aspectRatio"></param>
        /// <returns></returns>
        public TilemapRenderer(int tileNum, bool isDynamic, float unitSize, float aspectRatio) : base(unitSize, aspectRatio)
        {
            _tileMap = Enumerable.Repeat<float>(0.0f, tileNum*4).ToArray();
            _isDynamic = isDynamic;
            
            Init(Resources.ReadText("MiCore2d.resources.instanced.vert"), Resources.ReadText("MiCore2d.resources.instanced.frag"));
            InitTilemap();
        }

        /// <summary>
        /// InitTilemap.
        /// </summary>
        protected virtual void InitTilemap()
        {
            BufferUsageHint hint = BufferUsageHint.StaticDraw;
            if (_isDynamic)
            {
                hint = BufferUsageHint.DynamicDraw;
            }

            GL.BindVertexArray(vertexArrayObject);

            tilemapBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, tilemapBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _tileMap.Length * sizeof(float), _tileMap, hint);

            shader.Use();

            int offsetLocation = shader.GetAttribLocation("aOffset");
            GL.EnableVertexAttribArray(offsetLocation);
            GL.VertexAttribPointer(offsetLocation, 4, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            GL.VertexAttribDivisor(offsetLocation, 1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// GetTilemap.
        /// </summary>
        /// <returns>map array data</returns>
        public float[] GetTileMap()
        {
            return _tileMap!;
        }

        public void SetTileMap(float[] tileMap)
        {
            _tileMap = tileMap;
        }

        /// <summary>
        /// UpdateTilemap.
        /// </summary>
        private void updateTilemap()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, tilemapBufferObject);
            GL.BufferSubData(BufferTarget.ArrayBuffer, 0, _tileMap.Length * sizeof(float), _tileMap);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        /// <summary>
        /// DrawElement.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        protected override void DrawElement(Camera camera, Element element)
        {
            if (_tileMap == null)
            {
                return;
            }
            Matrix4 model = (element.Rotation * Matrix4.CreateScale(element.Scale)) * Matrix4.CreateTranslation(element.Position);

            if (_isDynamic)
            {
                updateTilemap();
            }

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetFloat("texAlpha", (1.0f - element.Alpha));

            DrawInstanced(_tileMap.Length/4);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);
            base.Dispose(true);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    GL.DeleteBuffer(tilemapBufferObject);
                }
                _disposed = true;
            }
        }

        ~TilemapRenderer()
        {
            Dispose(false);
        }
    }
}
