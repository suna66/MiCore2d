using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;
using StbImageSharp;
using SkiaSharp;

namespace MiCore2d
{
    public class Texture2dArray : Texture
    {
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
                    ImageResult image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
                    GL.TexSubImage3D(TextureTarget.Texture2DArray,
                    0, 0, 0, i,
                    width, height, 1,
                    PixelFormat.Rgba,
                    PixelType.UnsignedByte,
                    image.Data);
                }
            }
            Width = width;
            Height = height;
            SetTexParameter();
            UnBind();
            textureCount = files.Length;
        }
    }
}