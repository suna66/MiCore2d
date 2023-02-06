using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// Texture.
    /// </summary>
    public abstract class Texture : IDisposable
    {
        /// <summary>
        /// Handle.
        /// </summary>
        protected int Handle;

        /// <summary>
        /// Width.
        /// </summary>
        /// <value>textrue width</value>
        public int Width { get; set;}

        /// <summary>
        /// Height
        /// </summary>
        /// <value>texture height</value>
        public int Height { get; set;}

        /// <summary>
        /// textureCoount.
        /// </summary>
        protected int textureCount;

        /// <summary>
        /// TextureCount
        /// </summary>
        /// <value>texture count</value>
        public int TextureCount { get => textureCount; }

        /// <summary>
        /// target.
        /// </summary>
        protected TextureTarget target;

        private bool _disposed = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="texTarget">texture target</param>
        public Texture(TextureTarget texTarget)
        {
            target = texTarget;
        }

        /// <summary>
        /// Genhandle.
        /// </summary>
        protected void GenHandle()
        {
            GenHandle(target);
        }

        /// <summary>
        /// GenHandle.
        /// </summary>
        /// <param name="texTarget">texture target</param>
        protected void GenHandle(TextureTarget texTarget)
        {
            Handle = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(texTarget, Handle);
        }

        /// <summary>
        /// SetTexParameter.
        /// </summary>
        protected void SetTexParameter()
        {
            SetTexParameter(target);
        }

        /// <summary>
        /// TexTexParameter.
        /// </summary>
        /// <param name="target">texture target</param>
        protected void SetTexParameter(TextureTarget target)
        {
            GL.TexParameter(target, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(target, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            GL.TexParameter(target, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(target, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            if (target == TextureTarget.Texture2D)
            {
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }
            else
            {
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2DArray);
            }
        }

        /// <summary>
        /// Bind.
        /// </summary>
        protected void Bind()
        {
            Bind(target);
        }

        /// <summary>
        /// Bind.
        /// </summary>
        /// <param name="texTarget">texture target</param>
        protected void Bind(TextureTarget texTarget)
        {
            GL.BindTexture(texTarget, Handle);
        }

        /// <summary>
        /// UnBind.
        /// </summary>
        protected void UnBind()
        {
            UnBind(target);
        }

        /// <summary>
        /// UnBind.
        /// </summary>
        /// <param name="texTarget">texture target</param>
        protected void UnBind(TextureTarget texTarget)
        {
            GL.BindTexture(texTarget, 0);
        }

        /// <summary>
        /// Use
        /// </summary>
        public void Use()
        {
            Use(TextureUnit.Texture0);
        }

        /// <summary>
        /// Use.
        /// </summary>
        /// <param name="unit">texture unit</param>
        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(target, Handle);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    GL.DeleteTexture(Handle);
                }
                _disposed = true;
            }
        }

        ~Texture()
        {
            Dispose(false);
        }
    }
}