using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    public abstract class Texture : IDisposable
    {
        protected int Handle;

        public int Width { get; set;}
        public int Height { get; set;}

        protected int textureCount;
        public int TextureCount { get => textureCount; }

        protected TextureTarget target;

        private bool _disposed = false;

        public Texture(TextureTarget texTarget)
        {
            target = texTarget;
        }

        protected void GenHandle()
        {
            GenHandle(target);
        }

        protected void GenHandle(TextureTarget texTarget)
        {
            Handle = GL.GenTexture();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(texTarget, Handle);
        }

        protected void SetTexParameter()
        {
            SetTexParameter(target);
        }

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

        protected void Bind()
        {
            Bind(target);
        }

        protected void Bind(TextureTarget texTarget)
        {
            GL.BindTexture(texTarget, Handle);
        }

        protected void UnBind()
        {
            UnBind(target);
        }

        protected void UnBind(TextureTarget texTarget)
        {
            GL.BindTexture(texTarget, 0);
        }

        public void Use()
        {
            Use(TextureUnit.Texture0);
        }

        public void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(target, Handle);
        }

        public void Dispose()
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