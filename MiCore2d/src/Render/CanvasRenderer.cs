using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SkiaSharp;

namespace MiCore2d
{
    /// <summary>
    /// TextureRenderer
    /// </summary>
    public class CanvasRenderer : Renderer
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CanvasRenderer()
        {
            //LoadShader("resources/basic.vert", "resources/basic.frag");
            LoadShader(Resources.ReadText("MiCore2d.resources.basic.vert"), Resources.ReadText("MiCore2d.resources.basic.frag"));
            Init();
        }

        /// <summary>
        /// DrawElement.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        protected override void DrawElement(Camera camera, Element element)
        {
            if (element is not CanvasSprite)
            {
                return;
            }
            CanvasSprite sprite = (CanvasSprite)element;
            sprite.Flush();

            Matrix4 model = (element.Rotation * Matrix4.CreateScale(element.Scale)) * Matrix4.CreateTranslation(element.Position);

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetFloat("texAlpha", (1.0f - element.Alpha));

            Draw();
        }

    }
}