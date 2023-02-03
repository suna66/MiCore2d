using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.TileMap
{
    public class ObstacleRenderer : InstancedRenderer
    {
        protected override float[] CreateMapData()
        {
            float[] map = {
                -4.0f,  3.0f,  0.0f, 102.0f,
                -3.0f,  3.0f,  0.0f, 102.0f,
                -2.0f,  3.0f,  0.0f, 102.0f,
                -1.0f,  3.0f,  0.0f, 102.0f,
                 0.0f,  3.0f,  0.0f, 102.0f,
                 1.0f,  3.0f,  0.0f, 102.0f,
                 2.0f,  3.0f,  0.0f, 102.0f,
                 3.0f,  3.0f,  0.0f, 102.0f,
                 4.0f,  3.0f,  0.0f, 102.0f,
                -4.0f,  2.0f,  0.0f, 102.0f,
                -4.0f,  1.0f,  0.0f, 102.0f,
                -4.0f,  0.0f,  0.0f, 102.0f,
                -4.0f, -1.0f,  0.0f, 102.0f,
                -4.0f, -2.0f,  0.0f, 102.0f,
                -4.0f, -3.0f,  0.0f, 102.0f,
                 4.0f,  2.0f,  0.0f, 102.0f,
                 4.0f,  1.0f,  0.0f, 102.0f,
                 4.0f,  0.0f,  0.0f, 102.0f, 
                 4.0f, -1.0f,  0.0f, 102.0f,
                 4.0f, -2.0f,  0.0f, 102.0f,
                 4.0f, -3.0f,  0.0f, 102.0f,
                -4.0f, -4.0f,  0.0f, 102.0f,
                -3.0f, -4.0f,  0.0f, 102.0f,
                -2.0f, -4.0f,  0.0f, 102.0f,
                -1.0f, -4.0f,  0.0f, 102.0f,
                 0.0f, -4.0f,  0.0f, 102.0f,
                 1.0f, -4.0f,  0.0f, 102.0f,
                 2.0f, -4.0f,  0.0f, 102.0f,
                 3.0f, -4.0f,  0.0f, 102.0f,
                 4.0f, -4.0f,  0.0f, 102.0f
            };
            return map;
        }

        protected override bool GetDynamic()
        {
            return false;
        }
    }
}