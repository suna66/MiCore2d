#nullable disable warnings
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;

namespace MiCore2d
{
    /// <summary>
    /// Tracking. tracking other element or camera.
    /// </summary>
    public class Tracking : Component
    {
        private Element _target;

        private Camera _camera;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Tracking()
        {
            _target = null;
            _camera = null;
        }


        /// <summary>
        /// TargetElement
        /// </summary>
        /// <value>element</value>
        public Element TargetElement
        {
            set => _target = value;
        }

        /// <summary>
        /// TargetCamera
        /// </summary>
        /// <value></value>
        public Camera TargetCamera
        {
            set => _camera = value;
        }

        /// <summary>
        /// UpdateComponent. called by game engine.
        /// </summary>
        /// <param name="elapsed">elpased time of frame.</param>
        public override void UpdateComponent(double elapsed)
        {
            if (_target != null)
            {
                element.GlobalPosition = _target.GlobalPosition;
            }
            else if (_camera != null)
            {
                // Vector3 camera = _camera.Position;
                // camera.Z = element.GlobalPosition.Z;
                // element.GlobalPosition = camera;
                element.SetPosition(_camera.Position.X, _camera.Position.Y, element.GlobalPosition.Z);
            }
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