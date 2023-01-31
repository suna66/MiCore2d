using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class InstancedRenderer : Renderer
    {
        private float[] _tilemap;

        protected int tilemapBufferObject;

        private bool _disposed = false;

        private bool _isDynamic = false;

        public InstancedRenderer(float [] tilemap, bool is_dynamic)
        {
            //LoadShader("resources/instanced.vert", "resources/instanced.frag");
            LoadShader(Resources.ReadText("MiCore2d.resources.instanced.vert"), Resources.ReadText("MiCore2d.resources.instanced.frag"));

            _tilemap = tilemap;
            BufferUsageHint hint = BufferUsageHint.StaticDraw;
            if (is_dynamic)
            {
                hint = BufferUsageHint.DynamicDraw;
                _isDynamic = true;
            }
            Init();
            InitTilemap(hint);
        }

        protected void InitTilemap(BufferUsageHint hint)
        {
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

        public void UpdateTilemap(float[] tilemap)
        {
            _tilemap = tilemap;
            GL.BindVertexArray(vertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, tilemapBufferObject);
            GL.BufferSubData(BufferTarget.ArrayBuffer, 0, _tilemap.Length * sizeof(float), _tilemap);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        protected override void DrawElement(Camera camera, Element element)
        {
            Matrix4 model = (element.Rotation * Matrix4.CreateScale(element.Scale)) * Matrix4.CreateTranslation(element.Position);

            if (_isDynamic)
            {
                if (element is TilemapSprite)
                {
                    float[] map = ((TilemapSprite)element).GetPosisionMap();
                    UpdateTilemap(map);
                }
            }

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetFloat("texAlpha", (1.0f - element.Alpha));

            DrawInstanced(_tilemap.Length/4);
        }

        public override void Dispose()
        {
            Dispose(true);
            base.Dispose(true);
        }

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
