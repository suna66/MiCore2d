using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class GameMain
    {
        private string _title;
        private int _width = 0;
        private int _height = 0;

        public int UnitCount {get; set;} = 5;
        
        public Color4 ClearColor {get; set;} = Color4.Black;

        public GameMain(string title, int width, int height)
        {
            _title = title;
            _width = width;
            _height = height;
        }

        public void Run(GameScene startScene)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(_width, _height),
                Title = _title,
                Flags = ContextFlags.ForwardCompatible,
            };

            using (Window win = new Window(GameWindowSettings.Default, nativeWindowSettings, startScene))
            {
                win.ClearColor = ClearColor;
                win.UnitCount = UnitCount;
                win.Run();
            }
        }
    }
}