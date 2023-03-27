using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// NoiseRenderer
    /// </summary>
    public class CircleRenderer : Renderer
    {
        /// <summary>
        /// Radius
        /// </summary>
        /// <value>float</value>
        public float Radius {get; set;} = 5.0f;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public CircleRenderer()
        {
            //LoadShader("resources/basic.vert", "resources/sepia.frag");
            LoadShader(Resources.ReadText("MiCore2d.resources.basic.vert"), Resources.ReadText("MiCore2d.resources.circle.frag"));
            Init();
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
            //shader.SetFloat("texAlpha", (1.0f - element.Alpha));
            shader.SetVector2("r", new Vector2(element.Scale.X, element.Scale.Y));
            shader.SetFloat("radius", Radius);

            Draw();
        }
    }
}