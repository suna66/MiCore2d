using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// NoiseRenderer
    /// </summary>
    public class CircleEffectRenderer : Renderer
    {
        /// <summary>
        /// Radius
        /// </summary>
        /// <value>float</value>
        public float Radius {get; set;} = 5.0f;

        public Vector2 Centor {get; set;} = Vector2.Zero;

        /// <summary>
        /// Order.
        /// </summary>
        /// <value>0: forward order, -1: Reverse order</value>
        public float Order {get; set;} = 0.0f;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public CircleEffectRenderer(float unitSize, float aspectRatio) : base(unitSize, aspectRatio)
        {
            Init(Resources.ReadText("MiCore2d.resources.basic.vert"), Resources.ReadText("MiCore2d.resources.circle_effect.frag"));
        }

        /// <summary>
        /// DrawElement.
        /// </summary>
        /// <param name="camera">camera</param>
        /// <param name="element">element</param>
        protected override void DrawElement(Camera camera, Element element)
        {
            Matrix4 model = (element.Rotation * Matrix4.CreateScale(element.Scale)) * Matrix4.CreateTranslation(element.Position);

            //Vector2 resolution = new Vector2(element.Scale.X, element.Scale.Y);
            Vector2  resolution = new Vector2(element.Width, element.Height);

            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.GetViewMatrix());
            shader.SetMatrix4("projection", camera.GetProjectionMatrix());
            //shader.SetFloat("texAlpha", (1.0f - element.Alpha));
            shader.SetVector2("r", resolution);
            shader.SetFloat("radius", Radius);
            shader.SetVector2("centor", Centor);
            shader.SetFloat("order", Order);

            Draw();
        }
    }
}