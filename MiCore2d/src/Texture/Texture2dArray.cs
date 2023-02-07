using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// Texture2dArray.
    /// </summary>
    public class Texture2dArray : Texture
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="files">file list</param>
        /// <param name="width">width of an image file.</param>
        /// <param name="height">height of an image file.</param>
        /// <returns>instance</returns>
        public Texture2dArray(string[] files, int width, int height) : base(TextureTarget.Texture2DArray)
        {
            if (files == null || files.Length == 0)
            {
                throw new ArgumentException("parameter is null or zero");
            }
            GenHandle();
            GL.TexImage3D(
                TextureTarget.Texture2DArray,
                0,
                PixelInternalFormat.Rgba,
                width, height,
                files.Length,
                0,
                PixelFormat.Rgba, PixelType.UnsignedByte,
                (IntPtr)0
            );
            for (int i = 0; i < files.Length; i++)
            {
                using (Stream stream = File.OpenRead(files[i]))
                {
                    loadTexture(stream, i);
                }
            }
            Width = width;
            Height = height;
            SetTexParameter();
            UnBind();
            textureCount = files.Length;
        }

        /// <summary>
        /// Constructor. Create blank texture array.
        /// </summary>
        /// <param name="array_size">size of array</param>
        /// <param name="width">width of an image file.</param>
        /// <param name="height">height of an image file.</param>
        /// <returns>instance</returns>
        public Texture2dArray(int array_size, int width, int height) : base(TextureTarget.Texture2DArray)
        {
            if (array_size <= 0)
            {
                throw new ArgumentException("texture array size is zero");
            }
            GenHandle();
            GL.TexImage3D(
                TextureTarget.Texture2DArray,
                0,
                PixelInternalFormat.Rgba,
                width, height,
                array_size,
                0,
                PixelFormat.Rgba, PixelType.UnsignedByte,
                (IntPtr)0
            );
            Width = width;
            Height = height;
            SetTexParameter();
            UnBind();
            textureCount = array_size;
        }

        /// <summary>
        /// AddTexture.
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="index">index</param>
        public void AddTexture(Stream stream, int index)
        {
            if (index >= textureCount)
            {
                throw new ArgumentOutOfRangeException("index is over for texture count.");
            }
            Bind();
            loadTexture(stream, index);
            UnBind();
        }


        /// <summary>
        /// AddTexture.
        /// </summary>
        /// <param name="file">file path</param>
        /// <param name="index">index</param>
        public void AddTexture(string file, int index)
        {
            if (index >= textureCount)
            {
                throw new ArgumentOutOfRangeException("index is over for texture count.");
            }
            Bind();
            using (Stream stream = File.OpenRead(file))
            {
                loadTexture(stream, index);
            }
            UnBind();
        }

        /// <summary>
        /// loadTexture.
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="index">index</param>
        private void loadTexture(Stream stream, int index)
        {
            ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
            GL.TexSubImage3D(TextureTarget.Texture2DArray,
                0, 0, 0, index,
                Width, Height, 1,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                image.Data);
        }
    }
}