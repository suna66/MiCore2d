using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// TextureRenderer
    /// </summary>
    public class SwipeRenderer : Renderer
    {
        public float Scale {get; set; } = 0.0f;

        public float Direct {get; set;} = 0.0f;

        /// <summary>
        /// Constructor.
        /// </summary>
        public SwipeRenderer()
        {
            Init(Resources.ReadText("MiCore2d.resources.basic.vert"), Resources.ReadText("MiCore2d.resources.swipe.frag"));
        }

        /// <summary>
        /// DrawElement.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        protected override void DrawElement(Camera camera, Element element)
        {
            Matrix4 model = (element.Rotation * Matrix4.CreateScale(element.Scale)) * Matrix4.CreateTranslation(element.Position);

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetFloat("texAlpha", (1.0f - element.Alpha));
            shader.SetFloat("scale", Scale);
            shader.SetFloat("direct", Direct);

            Draw();
        }
    }
}