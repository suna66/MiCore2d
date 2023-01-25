using OpenTK.Mathematics;

namespace MiCore2d
{
    public class BlankSprite : Element
    {
        public BlankSprite(float unitSize) : base()
        {
            texture = null;

            scale.X = unitSize;
            scale.Y = unitSize;
            unit = unitSize;
            RendererName = "sprite";
        }
    }
}