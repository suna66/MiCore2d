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
    /// <summary>
    /// Window
    /// </summary>
    public class Window : GameWindow
    {
        private GameScene? _startScene = null;
        private GameControl _control;

        private Color4 _clearColor = Color4.Black;

        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="gameWindowSettings">game window settings</param>
        /// <param name="nativeWindowSettings">navite windows settings</param>
        /// <param name="startScene">start game scene</param>
        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, GameScene startScene)
          : base(gameWindowSettings, nativeWindowSettings)
        {
            _startScene = startScene;
        }

        /// <summary>
        /// ClearColor
        /// </summary>
        /// <value>color</value>
        public Color4 ClearColor {
            get => _clearColor;
            set
            {
                _clearColor = value;
            }
        }

        /// <summary>
        /// UnitCount.
        /// </summary>
        /// <value>number of half height unit</value>
        public int UnitCount {get; set;} = 5;

        /// <summary>
        /// OnLoad.
        /// </summary>
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

        /// <summary>
        /// OnUnLoad.
        /// </summary>
        protected override void OnUnload()
        {
            _control.OnUnload();
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
        }

        /// <summary>
        /// OnResize.
        /// </summary>
        /// <param name="e">parameter</param>
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                GL.Viewport(0, 0, Size.X, Size.Y);
            }
            _control.OnResize(e);
        }

        /// <summary>
        /// OnRandererFrame.
        /// </summary>
        /// <param name="e">parameter</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            _control.OnRenderFrame(e);

            SwapBuffers();
        }

        /// <summary>
        /// OnUpdateFrame
        /// </summary>
        /// <param name="e">parameter</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            _control.OnUpdateFrame(e);
        }

        /// <summary>
        /// OnMoouseUp
        /// </summary>
        /// <param name="e">parameter</param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

        /// <summary>
        /// OnMouseDown
        /// </summary>
        /// <param name="e">parameter</param>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        /// <summary>
        /// OnMouseMove
        /// </summary>
        /// <param name="e">parameter</param>
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
        }

        /// <summary>
        /// OnMouseWheel.
        /// </summary>
        /// <param name="e">parameter</param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _control.OnMouseWheel(e);
        }

    }
}