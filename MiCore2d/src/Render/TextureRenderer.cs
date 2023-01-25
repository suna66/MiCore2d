using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class TextureRenderer : Renderer
    {
        public TextureRenderer()
        {
            //LoadShader("resources/basic.vert", "resources/basic.frag");
            LoadShader(Resources.ReadText("MiCore2d.resources.basic.vert"), Resources.ReadText("MiCore2d.resources.basic.frag"));
            Init();
        }

        protected override void DrawElement(Camera camera, Element element)
        {
            Matrix4 model = (element.Rotation * Matrix4.CreateScale(element.Scale)) * Matrix4.CreateTranslation(element.Position);

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetFloat("texAlpha", (1.0f - element.Alpha));

            Draw();
        }
    }
}