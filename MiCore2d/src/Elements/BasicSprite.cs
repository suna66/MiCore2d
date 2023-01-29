using OpenTK.Mathematics;

namespace MiCore2d
{
    public class BasicSprite : Element
    {
        private bool disposed = false;
        
        public BasicSprite(Texture tex, float unitSize) : base()
        {
            texture = tex;

            float aspectRatio = texture.Width / (float)texture.Height;
            scale.X = unitSize * aspectRatio;
            scale.Y = unitSize;
            unit = unitSize;
            RendererName = "sprite";
        }

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