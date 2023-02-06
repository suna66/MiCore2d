using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// ImageSprite.
    /// </summary>
    public class ImageSprite : Element
    {
        private bool disposed = false;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tex">texture</param>
        /// <param name="unitSize">unit size</param>
        /// <returns>ImageSprite</returns>
        public ImageSprite(Texture tex, float unitSize) : base()
        {
            texture = tex;

            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            unit = unitSize;
            if (tex is Texture2dArray || tex is Texture2dTile)
            {
                DrawRenderer = RendererManager.GetInstance().GetRenderer<TextureArrayRenderer>();
            }
            else
            {
                DrawRenderer = RendererManager.GetInstance().GetRenderer<TextureRenderer>();
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            if (!disposed)
            {
                base.Dispose();
                disposed = true;                
            }
        }
    }
}