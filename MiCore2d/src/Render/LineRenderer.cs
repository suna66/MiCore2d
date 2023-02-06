using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// LineRenderer.
    /// </summary>
    public class LineRenderer : Renderer
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LineRenderer()
        {
            LoadShader(Resources.ReadText("MiCore2d.resources.line.vert"), Resources.ReadText("MiCore2d.resources.line.frag"));
            Init();
        }

        /// <summary>
        /// Init.
        /// </summary>
        protected override void Init()
        {
            vertices = new float[]
            {
                -0.5f,  0.0f, 0.0f, 
                 0.5f,  0.0f, 0.0f
            };
            indices = new uint[]
            {
                0, 1
            };
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.DynamicDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            shader.Use();
            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        public override void Draw(Camera camera, Element element)
        {
            if (!element.Disabled && element.Visibled)
            {
                DrawElement(camera, element);
            }
        }

        /// <summary>
        /// DrawElement.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        protected override void DrawElement(Camera camera, Element element)
        {
            if (element is not LineSprite)
                return;
            
            LineSprite sprite = (LineSprite)element;

            GL.BindVertexArray(vertexArrayObject);
            updateVertics(sprite.StartPosition, sprite.EndPosition);
            shader.Use();
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetVector3("color", sprite.GetColor());

            DrawLine();

            GL.BindVertexArray(0);
        }

        /// <summary>
        /// updateVertics.
        /// </summary>
        /// <param name="startPos">start position</param>
        /// <param name="endPos">end position</param>
        private void updateVertics(Vector3 startPos, Vector3 endPos)
        {
            vertices[0] = startPos.X;
            vertices[1] = startPos.Y;
            vertices[2] = startPos.Z;
            vertices[3] = endPos.X;
            vertices[4] = endPos.Y;
            vertices[5] = endPos.Z;

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferSubData(BufferTarget.ArrayBuffer, 0, vertices.Length * sizeof(float), vertices);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}