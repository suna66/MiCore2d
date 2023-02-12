using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// CAMERA_TYPE
    /// </summary>
    public enum CAMERA_TYPE
    {
        PERSPECTIVE,
        ORTHONGRAPHIC,
    }

    /// <summary>
    /// Camera
    /// </summary>
    public class Camera
    {
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private float _pitch;

        private float _yaw = -MathHelper.PiOver2;
        private float _fov = MathHelper.PiOver2;
        private CAMERA_TYPE _cameraType = CAMERA_TYPE.PERSPECTIVE;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="position">Camera position</param>
        /// <param name="aspectRatio">aspect ratio</param>
        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        /// <summary>
        /// Position. Camera position.
        /// </summary>
        /// <value>camera position</value>
        public Vector3 Position { get; set; }

        /// <summary>
        /// SetPosition.
        /// </summary>
        /// <param name="pos">Vector2 position</param>
        public void SetPosition(Vector2 pos)
        {
            Vector3 position = Position;
            position.X = pos.X;
            position.Y = pos.Y;
            Position = position;
        }

        /// <summary>
        /// SetPositionX.
        /// </summary>
        /// <param name="x">x position</param>
        public void SetPositionX(float x)
        {
            Vector3 pos = Position;
            pos.X = x;
            Position = pos;
        }

        /// <summary>
        /// SetPositionY.
        /// </summary>
        /// <param name="y">y position</param>
        public void SetPositionY(float y)
        {
            Vector3 pos = Position;
            pos.Y = y;
            Position = pos;
        }

        /// <summary>
        /// SetPositionZ.
        /// </summary>
        /// <param name="Z">z position</param>
        public void SetPositionZ(float z)
        {
            Vector3 pos = Position;
            pos.Z = z;
            Position = pos;
        }

        /// <summary>
        /// AspectRatio. Camera aspect ratio.
        /// </summary>
        /// <value>aspect ratio</value>
        public float AspectRatio { private get; set; }

        /// <summary>
        /// Front. Direction of front.
        /// </summary>
        public Vector3 Front => _front;

        /// <summary>
        /// Up. Direction of Up.
        /// </summary>
        public Vector3 Up => _up;

        /// <summary>
        /// Right. Dirction of Right.
        /// </summary>
        public Vector3 Right => _right;

        /// <summary>
        /// CameraType.
        /// </summary>
        /// <value>CAMERA_TYPE</value>
        public CAMERA_TYPE CameraType
        {
            get => _cameraType;
            set
            {
                _cameraType = value;
            }
        }

        /// <summary>
        /// Pitch.
        /// </summary>
        /// <value>pitch</value>
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                float angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        /// <summary>
        /// Yaw.
        /// </summary>
        /// <value>yaw</value>
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        /// <summary>
        /// Fov.
        /// </summary>
        /// <value>fov</value>
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                float angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        /// <summary>
        /// GetViewMatrix.
        /// </summary>
        /// <returns>matrix</returns>
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

        /// <summary>
        /// GetProjectionMatrix.
        /// </summary>
        /// <returns>matrix</returns>
        public Matrix4 GetProjectionMatrix()
        {
            if (_cameraType == CAMERA_TYPE.PERSPECTIVE)
            {
                return createPerspectiveCameraView();
            }
            else
            {
                return createOrthographicCameraView();
            }
        }

        /// <summary>
        /// createPerspectiveCameraView.
        /// </summary>
        /// <returns>matrix</returns>
        private Matrix4 createPerspectiveCameraView()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
        }

        /// <summary>
        /// createOrthographicCameraView.
        /// </summary>
        /// <returns>metrix</returns>
        private Matrix4 createOrthographicCameraView()
        {
            return Matrix4.CreateOrthographic(Position.Z * 2 * AspectRatio, Position.Z * 2 , 0.01f, 100f);
        }

        /// <summary>
        /// SetAspectRatio.
        /// </summary>
        /// <param name="aspectRatio">aspect ratio</param>
        public void SetAspectRatio(float aspectRatio)
        {
            AspectRatio = aspectRatio;
        }

        /// <summary>
        /// UpdateVectors.
        /// </summary>
        private void UpdateVectors()
        {
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            _front = Vector3.Normalize(_front);

            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));

        }
    }
}
