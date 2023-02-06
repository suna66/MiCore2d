using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// TextureArrayRenderer
    /// </summary>
    public class TextureArrayRenderer : Renderer
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public TextureArrayRenderer()
        {
            //LoadShader("resources/arraytex.vert", "resources/arraytex.frag");
            LoadShader(Resources.ReadText("MiCore2d.resources.arraytex.vert"), Resources.ReadText("MiCore2d.resources.arraytex.frag"));
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
            shader.SetFloat("texIndex", (float)element.TextureIndex);

            Draw();
        }
    }
}