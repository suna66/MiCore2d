using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// PolygonRenderer.
    /// </summary>
    public class PolygonRenderer : Renderer
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PolygonRenderer()
        {
            //LoadShader("resources/basic.vert", "resources/basic.frag");
            LoadShader(Resources.ReadText("MiCore2d.resources.rect.vert"), Resources.ReadText("MiCore2d.resources.rect.frag"));
            Init();
        }

        /// <summary>
        /// Draw.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        public override void Draw(Camera camera, Element element)
        {
            if (!element.Disabled && element.Visibled)
            {
                DrawElement(camera, element);
            }
        }

        /// <summary>
        /// DrawElement.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        protected override void DrawElement(Camera camera, Element element)
        {
            Matrix4 model = (element.Rotation * Matrix4.CreateScale(element.Scale)) * Matrix4.CreateTranslation(element.Position);

            if (element is not PlainSprite)
                return;

            PlainSprite sprite = (PlainSprite)element;

            GL.BindVertexArray(vertexArrayObject);
            shader.Use();

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            shader.SetFloat("texAlpha", (1.0f - element.Alpha));
            shader.SetVector3("color", sprite.GetColor());

            Draw();

            GL.BindVertexArray(0);
        }
    }
}