#nullable disable warnings
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace MiCore2d
{
    /// <summary>
    /// GameSceneManager.
    /// </summary>
    public class GameSceneManager
    {
        private static GameScene? _currentScene = null;

        private static GameScene? _nextScene = null;

        private static bool _stackGameScene = false;

        private static Stack<GameScene>? _sceneStack = null;

        private static bool disposed = false;


        /// <summary>
        /// Init.
        /// </summary>
        public static void Init()
        {
            _sceneStack = new Stack<GameScene>();
        }

        /// <summary>
        /// SetStartScene.
        /// </summary>
        /// <param name="scene">start game scene instance</param>
        public static void SetStartScene(GameScene scene)
        {
            _currentScene = scene;
        }

        /// <summary>
        /// LoadScene.
        /// </summary>
        /// <param name="scene">scene instance</param>
        /// <param name="stackCurrentScene">stack current scene or not</param>
        public static void LoadScene(GameScene scene, bool stackCurrentScene)
        {
            _nextScene = scene;
            _stackGameScene = stackCurrentScene;
        }

        /// <summary>
        /// BackScene. Back game scene that was stacked before.
        /// </summary>
        public static void BackScene()
        {
            if (_sceneStack.Count == 0)
                return;
            
            _nextScene = _sceneStack.Pop();
            _stackGameScene = false;
        }

        /// <summary>
        /// HasNewScene.
        /// </summary>
        /// <returns>true: there is a new scene</returns>
        public static bool HasNewScene()
        {
            if (_nextScene != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Current.
        /// </summary>
        /// <value>current game scene.</value>
        public static GameScene Current { get => _currentScene; }

        /// <summary>
        /// SwitchScene.
        /// </summary>
        /// <returns>new scene</returns>
        public static GameScene SwitchScene()
        {
            if (_nextScene != null)
            {
                if (_stackGameScene)
                {
                    _sceneStack.Push(_currentScene);
                }
                else
                {
                    _currentScene.OnUnLoad();
                }
                _currentScene = _nextScene;
                _nextScene = null;
                _stackGameScene = false;
            }
            return _currentScene;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public static void Dispose()
        {
            if (!disposed)
            {
                foreach(GameScene scene in _sceneStack)
                {
                    scene.OnUnLoad();
                }
                if (_currentScene != null)
                {
                    _currentScene.OnUnLoad();
                }
                if (_nextScene != null)
                {
                    _nextScene.OnUnLoad();
                }
                disposed = true;
            }
        }
    }
}
