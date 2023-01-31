#nullable disable warnings
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using MiCore2d;

namespace Example.SceneChange
{
    public class FadeScript : Controller
    {
        public bool IsAnimation {get; set;} = false;

        private float _fadeVal = 1.0f;

        private GameScene? _nextScenet = null;

        // 0: fade-in
        // 1: fade-out
        private int _fadeType = 0;

        public override void Start()
        {
            IsAnimation = true;
            _fadeType = 0;
        }

        public bool SwitchScene(GameScene scene)
        {
            if (IsAnimation == true)
            {
                return false;
            }
            IsAnimation = true;
            _nextScenet = scene;
            _fadeType = 1;
            _fadeVal = 0.0f;

            return true;
        }

        public override void Update(double elapsed)
        {
            if (IsAnimation == false)
            {
                return;
            }

            if (_fadeType == 0)
            {
                _fadeVal -=  0.5f * (float)elapsed;
                if (_fadeVal < 0.0f)
                {
                    _fadeVal = 0.0f;
                    IsAnimation = false;
                }
                element.Alpha = _fadeVal;
            }
            else
            {
                _fadeVal += 0.5f * (float)elapsed;
                if (_fadeVal > 1.0f)
                {
                    _fadeVal = 1.0f;
                }
                element.Alpha = _fadeVal;
                if (_fadeVal >= 1.0f)
                {
                    GameSceneManager.LoadScene(_nextScenet, false);
                }
            }
        }

    }
}