#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;

namespace MiCore2d
{
    /// <summary>
    /// Physics.
    /// </summary>
    public class Physics
    {
        private static GameScene? _gameScene = null;

        /// <summary>
        /// SetGameScene. Setting game scene install using this class.
        /// </summary>
        /// <param name="scene">game scene</param>
        public static void SetGameScene(GameScene scene)
        {
            _gameScene = scene;
        }

        /// <summary>
        /// Raycast.
        /// </summary>
        /// <param name="position">paycast start position</param>
        /// <param name="direction">direction</param>
        /// <param name="distance">distance</param>
        /// <param name="layerMask">target layer</param>
        /// <returns>hidded element</returns>
        public static Element Raycast(Vector2 position, Vector2 direction, float distance, string layerMask = "default")
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
                Collider collider = target.GetComponent<Collider>();
                if (collider == null)
                {
                    continue;
                }
                if (collider.Collision(ray))
                {
                    return target;
                }
            }
            return null!;
        }

        /// <summary>
        /// Raycast.
        /// </summary>
        /// <param name="position">paycast start position</param>
        /// <param name="direction">direction</param>
        /// <param name="distance">distance</param>
        /// <param name="layerMask">target layer</param>
        /// <returns>hidded element</returns>
        public static Element Raycast(Vector3 position, Vector2 direction, float distance, string layerMask = "default")
        {
            if (_gameScene == null)
            {
                Log.Debug("GameScene is not set yet.");
                return null!;
            }

            Vector2 pos = new Vector2(position.X, position.Y);

            Line ray = new Line(pos, pos + direction * distance);

            IDictionaryEnumerator enumerator = _gameScene.GetElementEnumerator();
            while (enumerator.MoveNext())
            {
                Element target = (Element)enumerator.Value;
                if (target.Layer != layerMask)
                {
                    continue;
                }
                Collider collider = target.GetComponent<Collider>();
                if (collider == null)
                {
                    continue;
                }
                if (collider.Collision(ray))
                {
                    return target;
                }
            }
            return null!;
        }

        /// <summary>
        /// Pointcast.
        /// </summary>
        /// <param name="point">point of world spece</param>
        /// <param name="layerMask">target layer</param>
        /// <returns>hidded element</returns>
        public static Element Pointcast(Vector2 point, string layerMask = "default")
        {
            if (_gameScene == null)
            {
                Log.Debug("GameScene is no set yet");
                return null;
            }
            IDictionaryEnumerator enumerator = _gameScene.GetElementEnumerator();
            while (enumerator.MoveNext())
            {
                Element target = (Element)enumerator.Value;
                if (target.Layer != layerMask)
                {
                    continue;
                }
                Collider collider = target.GetComponent<Collider>();
                if (collider == null)
                {
                    continue;
                }
                if (collider.Collision(point))
                {
                    return target;
                }
            }
            return null!;
        }
    }
}
