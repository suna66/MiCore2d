
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// WaveTextureRenderer
    /// </summary>
    public class WaveTextureRenderer : Renderer
    {
        /// <summary>
        /// Elapsed time
        /// </summary>
        /// <value></value>
        public float Times {get; set;} = 0.0f;

        /// <summary>
        /// wave speed
        /// </summary>
        /// <value></value>
        public float Speed {get; set;} = 1.0f;

        /// <summary>
        /// wave length
        /// </summary>
        /// <value></value>
        public float Length {get; set;} = 0.1f;

        /// <summary>
        /// wave width
        /// </summary>
        /// <value></value>
        public float Width {get; set;} = 0.01f;

        /// <summary>
        /// Constructor.
        /// </summary>
        public WaveTextureRenderer()
        {
            //LoadShader("resources/basic.vert", "resources/sepia.frag");
            LoadShader(Resources.ReadText("MiCore2d.resources.basic.vert"), Resources.ReadText("MiCore2d.resources.wave.frag"));
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
            shader.SetFloat("texAlpha", (1.0f - element.Alpha));
            shader.SetFloat("speed", Speed);
            shader.SetFloat("length", Length);
            shader.SetFloat("width", Width);
            shader.SetFloat("time", Times);

            Draw();
        }
    }
}