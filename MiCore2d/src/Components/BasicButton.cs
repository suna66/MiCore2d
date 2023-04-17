#nullable disable warnings
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;

namespace MiCore2d
{
    /// <summary>
    /// ButtonAction.
    /// </summary>
    public class BasicButton : Component
    {
        private Action? _buttonDown;
        private Action? _buttonUp;
        private Action? _mouseEnter;
        private Action? _mouseLeave;

        private bool _pressState = false;
        private bool _previusePressSate = false;
        private bool _buttonState = false;
        private bool _mouseEnterState = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        public BasicButton()
        {
        }

        /// <summary>
        /// ButtonDown
        /// </summary>
        /// <value>action when button down</value>
        public Action ButtonDown
        {
            set
            {
                _buttonDown = value;
            }
        }

        /// <summary>
        /// ButtonUp
        /// </summary>
        /// <value>action when button up</value>
        public Action ButtonUp
        {
            set
            {
                _buttonUp = value;
            }
        }

        /// <summary>
        /// MouseEnter
        /// </summary>
        /// <value>actioin when mouse is entered</value>
        public Action MouseEnter
        {
            set
            {
                _mouseEnter = value;
            }
        }

        /// <summary>
        /// MouseLeave
        /// </summary>
        /// <value>action when mouse is leaved</value>
        public Action MouseLeave
        {
            set
            {
                _mouseLeave = value;
            }
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            MousePosition mouse = element.CurrentGameScene.MousePositionInfo;
            MouseButtonState state = element.CurrentGameScene.MouseStateInfo;

            _pressState = state.IsAnyPress;
            bool hit = CollisionUtil.PointBox(mouse.Position, element.Position, element.Width, element.Height);

            if (hit)
            {
                if (_pressState && !_previusePressSate)
                {
                    _buttonDown?.Invoke();
                    _buttonState = true;
                }
                if (!_pressState && _previusePressSate)
                {
                    _buttonUp?.Invoke();
                    _buttonState = false;
                }
                if (!_mouseEnterState)
                {
                    _mouseEnter?.Invoke();
                }
            }
            else
            {
                if (_buttonState)
                {
                    _buttonUp();
                    _buttonState = false;
                }
                if (_mouseEnterState)
                {
                    _mouseLeave?.Invoke();
                }

            }
            _previusePressSate = _pressState;
            _mouseEnterState = hit;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}