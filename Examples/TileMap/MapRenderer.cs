using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.TileMap
{
    public class MapRenderer : InstancedRenderer
    {
        protected override float[] CreateMapData()
        {
            float[] map = {
                -3.0f,  2.0f,  0.0f,  6.0f,
                -2.0f,  2.0f,  0.0f,  6.0f,
                -1.0f,  2.0f,  0.0f,  6.0f,
                 0.0f,  2.0f,  0.0f,  6.0f,
                 1.0f,  2.0f,  0.0f,  6.0f,
                 2.0f,  2.0f,  0.0f,  6.0f,
                 3.0f,  2.0f,  0.0f,  6.0f,
                -3.0f,  1.0f,  0.0f,  6.0f,
                -2.0f,  1.0f,  0.0f,  0.0f,
                -1.0f,  1.0f,  0.0f,  0.0f,
                 0.0f,  1.0f,  0.0f,  0.0f,
                 1.0f,  1.0f,  0.0f,  0.0f,
                 2.0f,  1.0f,  0.0f,  0.0f,
                 3.0f,  1.0f,  0.0f,  6.0f,
                -3.0f,  0.0f,  0.0f,  6.0f,
                -2.0f,  0.0f,  0.0f,  0.0f,
                -1.0f,  0.0f,  0.0f,  0.0f,
                 0.0f,  0.0f,  0.0f,  0.0f,
                 1.0f,  0.0f,  0.0f,  0.0f,
                 2.0f,  0.0f,  0.0f,  0.0f,
                 3.0f,  0.0f,  0.0f,  6.0f,
                -3.0f, -1.0f,  0.0f,  6.0f,
                -2.0f, -1.0f,  0.0f,  0.0f,
                -1.0f, -1.0f,  0.0f,  0.0f,
                 0.0f, -1.0f,  0.0f,  0.0f,
                 1.0f, -1.0f,  0.0f,  0.0f,
                 2.0f, -1.0f,  0.0f,  0.0f,
                 3.0f, -1.0f,  0.0f,  6.0f,
                -3.0f, -2.0f,  0.0f,  6.0f,
                -2.0f, -2.0f,  0.0f,  0.0f,
                -1.0f, -2.0f,  0.0f,  0.0f,
                 0.0f, -2.0f,  0.0f,  0.0f,
                 1.0f, -2.0f,  0.0f,  0.0f,
                 2.0f, -2.0f,  0.0f,  0.0f,
                 3.0f, -2.0f,  0.0f,  6.0f,
                -3.0f, -3.0f,  0.0f,  6.0f,
                -2.0f, -3.0f,  0.0f,  6.0f,
                -1.0f, -3.0f,  0.0f,  6.0f,
                 0.0f, -3.0f,  0.0f,  6.0f,
                 1.0f, -3.0f,  0.0f,  6.0f,
                 2.0f, -3.0f,  0.0f,  6.0f,
                 3.0f, -3.0f,  0.0f,  6.0f
                 //3.0f, -3.0f,  0.0f, 102.0f
            };
            return map;
        }

        protected override bool GetDynamic()
        {
            return false;
        }
    }
}