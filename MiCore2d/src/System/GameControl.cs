#nullable disable warnings
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using MiCore2d.Audio;

namespace MiCore2d
{
    /// <summary>
    /// GameControl
    /// </summary>
    public class GameControl
    {
        private GameWindow? _win = null!;

        private AudioSource? _audio = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="win">GameWindow</param>
        /// <param name="scene">GameScene</param>
        public GameControl(GameWindow win, GameScene scene)
        {
            _win = win;
            _audio = new AudioSource();
            GameSceneManager.Init();
            GameSceneManager.SetStartScene(scene);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="win">GameWindow</param>
        /// <param name="scene">GameScene</param>
        /// <param name="unitCount">number of half height unit</param>
        public GameControl(GameWindow win, GameScene scene, int unitCount)
        {
            _win = win;
            _audio = new AudioSource();
            UnitCount = unitCount;
            GameSceneManager.Init();
            GameSceneManager.SetStartScene(scene);
        }

        /// <summary>
        /// GW.
        /// </summary>
        /// <value>GameWindow</value>
        public GameWindow GW { get => _win; }

        /// <summary>
        /// Audio.
        /// </summary>
        /// <value>AudioSource</value>
        public AudioSource Audio { get => _audio; }

        /// <summary>
        /// UnitCount.
        /// </summary>
        /// <value>half height unit count</value>
        public int UnitCount { get; set; } = 5;

        /// <summary>
        /// OnLoad
        /// </summary>
        public void OnLoad()
        {
            if (GameSceneManager.Current != null)
            {
                GameSceneManager.Current.Init(this);
                GameSceneManager.Current.OnLoad();
            }
        }

        /// <summary>
        /// OnUnLoad.
        /// </summary>
        public void OnUnload()
        {
            GameSceneManager.Dispose();
        }

        /// <summary>
        /// OnResize.
        /// </summary>
        /// <param name="e">parameter</param>
        public void OnResize(ResizeEventArgs e)
        {
            if (GameSceneManager.Current != null)
                GameSceneManager.Current.OnResize(e);
        }

        /// <summary>
        /// OnRandererFrame.
        /// </summary>
        /// <param name="e">parameter</param>
        public void OnRenderFrame(FrameEventArgs e)
        {
            if (GameSceneManager.HasNewScene())
            {
                GameScene game = GameSceneManager.SwitchScene();
                game.Init(this);
                game.OnLoad();
            }
            if (GameSceneManager.Current != null)
                GameSceneManager.Current.OnRenderer(e.Time);
        }

        /// <summary>
        /// OnUpdateFrame
        /// </summary>
        /// <param name="e">parameter</param>
        public void OnUpdateFrame(FrameEventArgs e)
        {
            if (GameSceneManager.HasNewScene())
            {
                GameScene game = GameSceneManager.SwitchScene();
                game.Init(this);
                game.OnLoad();
            }
            if (GameSceneManager.Current != null)
            {
                GameSceneManager.Current.OnUpdate(e.Time);
                GameSceneManager.Current.OnAfterUpdate(e.Time);
            }
        }

        /// <summary>
        /// OnMoouseUp
        /// </summary>
        /// <param name="e">parameter</param>
        public void OnMouseUp(MouseButtonEventArgs e)
        {
            if (GameSceneManager.Current != null)
                GameSceneManager.Current.OnMouseButton(e.Button, e.IsPressed);
        }

        /// <summary>
        /// OnMouseDown
        /// </summary>
        /// <param name="e">parameter</param>
        public void OnMouseDown(MouseButtonEventArgs e)
        {
            if (GameSceneManager.Current != null)
                GameSceneManager.Current.OnMouseButton(e.Button, e.IsPressed);
        }

        /// <summary>
        /// OnMouseMove
        /// </summary>
        /// <param name="e">parameter</param>
        public void OnMouseMove(MouseMoveEventArgs e)
        {
            if (GameSceneManager.Current != null)
                GameSceneManager.Current.OnMouseMove(e.X, e.Y, e.DeltaX, e.DeltaY);
        }

        /// <summary>
        /// OnMouseWheel.
        /// </summary>
        /// <param name="e">parameter</param>
        public void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (GameSceneManager.Current != null)
                GameSceneManager.Current.OnMouseWheel(e.OffsetX, e.OffsetY);
        }
    }
}