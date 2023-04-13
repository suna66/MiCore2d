using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Renderer.
    /// </summary>
    public abstract class Renderer : IDisposable
    {
        protected int elementBufferObject;
        protected int vertexBufferObject;
        protected int vertexArrayObject;
        protected Shader shader = null!;
        //default vertices.
        protected float[] vertices = new float[]
            {
                 0.5f,  0.5f, 0.0f, 1.0f, 0.0f,
                 0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
                -0.5f, -0.5f, 0.0f, 0.0f, 1.0f,
                -0.5f,  0.5f, 0.0f, 0.0f, 0.0f
            };
        //default indices
        protected uint[] indices = new uint[]
            {
                0, 1, 3,
                1, 2, 3
            };

        private bool _disposed = false;
 
        /// <summary>
        /// Constructor.
        /// </summary>
        public Renderer()
        {
        }

        /// <summary>
        /// Init.
        /// </summary>
        protected virtual void Init(string vartString, string fragString)
        {
            shader = new Shader(vartString, fragString);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            shader.Use();
            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            int texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// DrawElement.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        protected abstract void DrawElement(Camera camera, Element element);

        /// <summary>
        /// Rendering
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        public virtual void Rendering(Camera camera, Element element)
        {
            if (!element.Disabled && element.Visibled)
            {
                if (element.Texture == null)
                {
                    DrawElement(camera, element);
                }
                else
                {
                    Begin(element.Texture);
                    DrawElement(camera, element);
                    End();
                }
            }
        }

        /// <summary>
        /// Begin.
        /// </summary>
        /// <param name="texture">texture</param>
        protected void Begin(Texture texture)
        {
            GL.BindVertexArray(vertexArrayObject);
            texture.Use(TextureUnit.Texture0);
            shader.Use();
        }

        /// <summary>
        /// Draw.
        /// </summary>
        protected void Draw()
        {
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// DrawInstanced.
        /// </summary>
        /// <param name="instanceCount">number of instanced</param>
        protected void DrawInstanced(int instanceCount)
        {
            GL.DrawElementsInstanced(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0, instanceCount);
        }

        /// <summary>
        /// DrawLine.
        /// </summary>
        protected void DrawLine()
        {
            GL.DrawElements(PrimitiveType.Lines, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// End.
        /// </summary>
        public void End()
        {
            GL.BindVertexArray(0);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    GL.DeleteBuffer(vertexBufferObject);
                    GL.DeleteBuffer(elementBufferObject);
                    GL.DeleteVertexArray(vertexArrayObject);
                }
                _disposed = true;
            }
        }

        ~Renderer()
        {
            Dispose(false);
        }
    }
}