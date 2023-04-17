using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// BlurRenderer
    /// </summary>
    public class BlurRenderer : Renderer
    {
        /// <summary>
        /// Blur size
        /// </summary>
        /// <value>0.0f - 500.0</value>
        public float Blur {get; set;} = 0.0f;

        /// <summary>
        /// Bloom size
        /// </summary>
        /// <value>0.00-2.00</value>
        public float Bloom {get; set;} = 1.0f;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BlurRenderer(float unitSie, float aspectRatio) : base(unitSie, aspectRatio)
        {
            Init(Resources.ReadText("MiCore2d.resources.basic.vert"), Resources.ReadText("MiCore2d.resources.blur.frag"));
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
            shader.SetFloat("blur", Blur);
            shader.SetFloat("bloom", Bloom);
            shader.SetFloat("width", element.Texture.Width);
            shader.SetFloat("height", element.Texture.Height);

            Draw();
        }
    }
}