using OpenTK.Mathematics;

namespace MiCore2d
{
    public enum CAMERA_TYPE
    {
        PERSPECTIVE,
        ORTHONGRAPHIC,
    }
    public class Camera
    {
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private float _pitch;

        private float _yaw = -MathHelper.PiOver2;
        private float _fov = MathHelper.PiOver2;
        private CAMERA_TYPE _cameraType = CAMERA_TYPE.PERSPECTIVE;

        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        public Vector3 Position { get; set; }
        public float AspectRatio { private get; set; }

        public Vector3 Front => _front;
        public Vector3 Up => _up;
        public Vector3 Right => _right;

        public CAMERA_TYPE CameraType
        {
            get => _cameraType;
            set
            {
                _cameraType = value;
            }
        }

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

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                float angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

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

        private Matrix4 createPerspectiveCameraView()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
        }

        private Matrix4 createOrthographicCameraView()
        {
            return Matrix4.CreateOrthographic(Position.Z * 2 * AspectRatio, Position.Z * 2 , 0.01f, 100f);
        }

        public void SetAspectRatio(float aspectRatio)
        {
            AspectRatio = aspectRatio;
        }

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
