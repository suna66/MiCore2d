using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// BlankSprite.
    /// </summary>
    public class BlankSprite : Element
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="unitSize">unit size</param>
        /// <returns>BlankSprite</returns>
        public BlankSprite(float unitSize) : base()
        {
            texture = null;

            Unit = unitSize;
            AspectRatio = 1.0f;
            DrawRenderer = null;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="unitSize">unit size</param>
        /// <param name="aspectRatio">aspect ratio</param>
        /// <returns>BlankSprite</returns>
        public BlankSprite(float unitSize, float aspectRatio) : base()
        {
            texture = null;

            Unit = unitSize;
            AspectRatio = aspectRatio;
            DrawRenderer = null;
        }
    }
}