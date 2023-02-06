using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// InstanceRenderer.
    /// </summary>
    public abstract class InstancedRenderer : Renderer
    {
        private float[]? _tilemap = null;

        protected int tilemapBufferObject;

        private bool _disposed = false;

        private bool _isDynamic = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public InstancedRenderer()
        {
            LoadShader(Resources.ReadText("MiCore2d.resources.instanced.vert"), Resources.ReadText("MiCore2d.resources.instanced.frag"));
            Init();
            InitTilemap();
        }

        /// <summary>
        /// CreateMapData. Abstract method.
        /// </summary>
        /// <returns>map array data</returns>
        protected abstract float[] CreateMapData();

        /// <summary>
        /// GetDynamic. Abstract method.
        /// </summary>
        /// <returns>true: update map data dyamicaly, false: not</returns>
        protected abstract bool GetDynamic();

        /// <summary>
        /// InitTilemap.
        /// </summary>
        protected virtual void InitTilemap()
        {
            _tilemap = CreateMapData();
            _isDynamic = GetDynamic();

            BufferUsageHint hint = BufferUsageHint.StaticDraw;
            if (_isDynamic)
            {
                hint = BufferUsageHint.DynamicDraw;
            }

            GL.BindVertexArray(vertexArrayObject);

            tilemapBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, tilemapBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _tilemap.Length * sizeof(float), _tilemap, hint);

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
        public float[] GetTilemap()
        {
            return _tilemap!;
        }

        /// <summary>
        /// UpdateTilemap.
        /// </summary>
        /// <param name="tilemap">map array data</param>
        public void UpdateTilemap(float[] tilemap)
        {
            _tilemap = tilemap;
            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, tilemapBufferObject);
            GL.BufferSubData(BufferTarget.ArrayBuffer, 0, _tilemap.Length * sizeof(float), _tilemap);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// DrawElement.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        protected override void DrawElement(Camera camera, Element element)
        {
            if (_tilemap == null)
            {
                return;
            }
            Matrix4 model = (element.Rotation * Matrix4.CreateScale(element.Scale)) * Matrix4.CreateTranslation(element.Position);

            if (_isDynamic)
            {
                if (element is TilemapSprite)
                {
                    float[] map = ((TilemapSprite)element).GetPositionMap();
                    UpdateTilemap(map);
                }
            }

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetFloat("texAlpha", (1.0f - element.Alpha));

            DrawInstanced(_tilemap.Length/4);
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

        ~InstancedRenderer()
        {
            Dispose(false);
        }
    }
}
