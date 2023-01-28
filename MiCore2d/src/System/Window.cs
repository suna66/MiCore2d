#nullable disable warnings
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;
using System.Runtime.InteropServices;

namespace MiCore2d
{
    public class Window : GameWindow
    {
        private GameScene? _startScene = null;
        private GameControl _control;

        private Color4 _clearColor = Color4.Black;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, GameScene startScene)
          : base(gameWindowSettings, nativeWindowSettings)
        {
            _startScene = startScene;
        }

        public Color4 ClearColor {
            get => _clearColor;
            set
            {
                _clearColor = value;
            }
        }

        public int UnitCount {get; set;} = 5;

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(_clearColor.R, _clearColor.G, _clearColor.B, _clearColor.A);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            if (_startScene == null)
            {
                throw new ArgumentNullException("start scene obejct is null");
            }

            _control = new GameControl(this, _startScene);
            _control.OnLoad();
        }

        protected override void OnUnload()
        {
            _control.OnUnload();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GL.Viewport(0, 0, Size.X, Size.Y);
            }
            _control.OnResize(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _control.OnRenderFrame(e);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            _control.OnUpdateFrame(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _control.OnMouseWheel(e);
        }

    }
}