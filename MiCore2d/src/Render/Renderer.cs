using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
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
 
        public Renderer()
        {
        }

        protected virtual void LoadShader(string vartString, string fragString)
        {
            shader = new Shader(vartString, fragString);
        }

        protected virtual void Init()
        {
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

        protected abstract void DrawElement(Camera camera, Element element);

        public void Draw(Camera camera, Element element)
        {
            if (element.Visible)
            {
                Begin(element.Texture);

                DrawElement(camera, element);

                End();
            }
        }

        protected void Begin(Texture texture)
        {
            GL.BindVertexArray(vertexArrayObject);
            texture.Use(TextureUnit.Texture0);
            shader.Use();
        }

        protected void Draw()
        {
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        protected void DrawInstanced(int instanceCount)
        {
            GL.DrawElementsInstanced(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0, instanceCount);
        }

        public void End()
        {
            GL.BindVertexArray(0);
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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