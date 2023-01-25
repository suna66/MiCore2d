#nullable disable warnings
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using MiCore2d.Audio;

namespace MiCore2d
{
    public class GameControl
    {
        private GameWindow? _win = null!;

        private AudioSource? _audio = null;

        public GameControl(GameWindow win, GameScene scene)
        {
            _win = win;
            _audio = new AudioSource();
            GameSceneManager.Init();
            GameSceneManager.SetStartScene(scene);
        }

        public GameControl(GameWindow win, GameScene scene, int unitCount)
        {
            _win = win;
            _audio = new AudioSource();
            UnitCount = unitCount;
            GameSceneManager.Init();
            GameSceneManager.SetStartScene(scene);
        }

        public GameWindow GW { get => _win; }

        public AudioSource Audio { get => _audio; }

        public int UnitCount { get; set; } = 5;

        public void OnLoad()
        {
            if (GameSceneManager.Current != null)
            {
                GameSceneManager.Current.Init(this);
                GameSceneManager.Current.OnLoad();
            }
        }

        public void OnUnload()
        {
            GameSceneManager.Dispose();
        }

        public void OnResize(ResizeEventArgs e)
        {
            if (GameSceneManager.Current != null)
                GameSceneManager.Current.OnResize(e);
        }

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

        public void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (GameSceneManager.Current != null)
                GameSceneManager.Current.OnMouseWheel(e);
        }
    }
}