#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;

namespace MiCore2d
{
    public class Physics
    {
        private static GameScene? _gameScene = null;
        public static void SetGameScene(GameScene scene)
        {
            _gameScene = scene;
        }

        public static Element Raycast(Vector2 position, Vector2 direction, float distance, string layerMask)
        {
            if (_gameScene == null)
            {
                Log.Debug("GameScene is not set yet.");
                return null!;
            }

            Line ray = new Line(position, position + direction * distance);

            IDictionaryEnumerator enumerator = _gameScene.GetElementEnumerator();
            while (enumerator.MoveNext())
            {
                Element target = (Element)enumerator.Value;
                if (target.Layer != layerMask)
                {
                    continue;
                }
                Vector2 targetPos = target.Position2d;
                Vector3 scale = target.Scale;
                float widthUnit = scale.X / 2;
                float heightUnit = scale.Y / 2;

                Line[] targetLine = new Line[4];
                targetLine[0] = new Line(targetPos + new Vector2(-widthUnit, heightUnit), targetPos + new Vector2(widthUnit, heightUnit));
                targetLine[1] = new Line(targetPos + new Vector2(widthUnit, heightUnit), targetPos + new Vector2(widthUnit, -heightUnit));
                targetLine[2] = new Line(targetPos + new Vector2(widthUnit, -heightUnit), targetPos + new Vector2(-widthUnit, -heightUnit));
                targetLine[3] = new Line(targetPos + new Vector2(-widthUnit, -heightUnit), targetPos + new Vector2(-widthUnit, heightUnit));
                for (int i = 0; i < 4; i++)
                {
                    if (ray.LineCollision(targetLine[i]))
                    {
                        return target;
                    }
                }
            }
            return null!;
        }
    }
}
