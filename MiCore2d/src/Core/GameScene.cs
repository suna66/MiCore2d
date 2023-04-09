#nullable disable warnings
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Collections;
using System.Collections.Specialized;
using MiCore2d.Audio;

namespace MiCore2d
{
    /// <summary>
    /// GameScene. Base class of game scene.
    /// </summary>
    public abstract class GameScene
    {
        private const float _RendererOrderCheckPriod = 1.0f;

        private OrderedDictionary _elemetDic;
        
        private List<Element> _rendererOrderList;

        private float _rendererOrderCheckTimer = 0.0f;

        private Camera? _camera = null;

        private GameControl? _control = null;

        private Canvas? _canvas = null;

        /// <summary>
        /// GW. GameWindow instance.
        /// </summary>
        /// <value>GameWindow</value>
        public GameWindow GW { get => _control.GW; }

        /// <summary>
        /// KeyState. Getting KeyboardState.
        /// </summary>
        /// <value>KeyboardState</value>
        public KeyboardState KeyState { get => _control.GW.KeyboardState; }

        /// <summary>
        /// Audio. Getting audio management instance.
        /// </summary>
        /// <value>AudioSource</value>
        public AudioSource Audio { get => _control.Audio; }

        /// <summary>
        /// Control. Getting GameControl instance. 
        /// </summary>
        /// <value>GameControl</value>
        public GameControl Control { get => _control; }

        /// <summary>
        /// SceneCanvas.
        /// </summary>
        /// <value>Canvas</value>
        public Canvas SceneCanvas { get => _canvas; }

        /// <summary>
        /// SceneCamera.
        /// </summary>
        /// <value>Camera</value>
        public Camera SceneCamera { get => _camera; }

        /// <summary>
        /// CurrentTime.
        /// </summary>
        /// <value>elpased time since executing game scene.</value>
        public double CurrentTime { get; set; }

        public Element CameraTarget { get; set; } = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameScene()
        {
            _elemetDic = new OrderedDictionary();

            _rendererOrderList = new List<Element>();

            CurrentTime = 0;
        }

        /// <summary>
        /// Init.
        /// </summary>
        /// <param name="control">game control instance</param>
        public void Init(GameControl control)
        {
            _control = control;

            _camera = new Camera(Vector3.UnitZ * _control.UnitCount, GW.Size.X / (float)GW.Size.Y);
            _camera.CameraType = CAMERA_TYPE.ORTHONGRAPHIC;

            _canvas = new Canvas(GW.Size.X, GW.Size.Y);
        }

        /// <summary>
        /// OnLoad.
        /// </summary>
        public virtual void OnLoad()
        {
            Load();
        }

        /// <summary>
        /// Load. Abstruct method.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// OnUpload. called game control class every frame.
        /// </summary>
        /// <param name="elapsed">elapsed time</param>
        public void OnUpdate(double elapsed)
        {
            CurrentTime += elapsed;
            if (CameraTarget != null)
            {
                _camera.SetPosition(CameraTarget.Position2d);
            }
            Update(elapsed);
        }

        /// <summary>
        /// Update. Abstruct method. Called game control class every frame.
        /// </summary>
        /// <param name="elapsed">elapsed time</param>
        public virtual void Update(double elapsed)
        {
            if (KeyState.IsKeyDown(Keys.Escape))
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// OnUnLoad
        /// </summary>
        public virtual void OnUnLoad()
        {
            _elemetDic.Clear();
            _rendererOrderList.Clear();
            if (_canvas != null)
            {
                _canvas.Dispose();
            }
        }

        /// <summary>
        /// OnAfterUpdate.
        /// </summary>
        /// <param name="elapsed">elapsed time</param>
        public virtual void OnAfterUpdate(double elapsed)
        {
            bool hasDestory = false;
            string deleteKey = string.Empty;
            IDictionaryEnumerator enumerator = _elemetDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Element element = enumerator.Value as Element;

                if (element.Destroyed)
                {
                    element.Dispose();
                    hasDestory = true;
                    deleteKey = (string)enumerator.Key;
                    continue;
                }
                element.UpdateComponents(elapsed);
            }
            if (hasDestory)
            {
                //Delete destroyed element gradually.
                _elemetDic.Remove(deleteKey);
            }
        }

        /// <summary>
        /// OnResize.
        /// </summary>
        /// <param name="e">ResizeEventArgs</param>
        public virtual void OnResize(ResizeEventArgs e)
        {
            _camera.AspectRatio = GW.Size.X / (float)GW.Size.Y;
        }

        /// <summary>
        /// OnMouseButton.
        /// </summary>
        /// <param name="button">button info</param>
        /// <param name="pressed">pressed or not</param>
        public virtual void OnMouseButton(MouseButton button, bool pressed)
        {

        }

        /// <summary>
        /// OnMouseMove
        /// </summary>
        /// <param name="x">position x</param>
        /// <param name="y">position y</param>
        /// <param name="deltaX">delta x</param>
        /// <param name="deltaY">delta y</param>
        public virtual void OnMouseMove(float x, float y, float deltaX, float deltaY)
        {

        }

        /// <summary>
        /// OnMouseWheel.
        /// </summary>
        /// <param name="offsetX">offset x</param>
        /// <param name="offsetY">offset y</param>
        public virtual void OnMouseWheel(float offsetX, float offsetY)
        {

        }

        /// <summary>
        /// OnRenderer.
        /// </summary>
        /// <param name="elapsed">elapsed time</param>
        public virtual void OnRenderer(double elapsed)
        {
            // IDictionaryEnumerator enumerator = _elemetDic.GetEnumerator();

            // while (enumerator.MoveNext())
            // {
            //     Element element = enumerator.Value as Element;
            //     if (!element.Disabled && !element.Destroyed)
            //     {
            //         element.DrawRenderer?.Draw(_camera, element);
            //     }
            // }
            _rendererOrderCheckTimer += (float)elapsed;
            if (_rendererOrderCheckTimer > _RendererOrderCheckPriod)
            {
                _rendererOrderList.Sort((x, y) => x.RendererOrder - y.RendererOrder);
                _rendererOrderCheckTimer = 0.0f;
            }

            foreach (Element element in _rendererOrderList)
            {
                if (!element.Disabled && !element.Destroyed)
                {
                    element.DrawRenderer?.Draw(_camera, element);
                }
            }
            _canvas.Update();
        }

        /// <summary>
        /// AddElement. add element to scene.
        /// </summary>
        /// <param name="key">namagement name</param>
        /// <param name="element">element</param>
        /// <param name="layerName">layer name(default is "default")</param>
        /// <returns>added element</returns>
        public Element AddElement(string key, Element element, string layerName = "default")
        {
            element.CurrentGameScene = this;
            element.Name = key;
            element.Layer = layerName;
            _elemetDic.Add(key, element);
            _rendererOrderList.Add(element);
            return element;
        }

        /// <summary>
        /// GetElement. Getting element from managed data.
        /// </summary>
        /// <param name="key">management name</param>
        /// <returns>Element</returns>
        public Element GetElement(string key)
        {
            if (!_elemetDic.Contains(key))
            {
                throw new KeyNotFoundException(key);
            }
            return _elemetDic[key] as Element;
        }

        /// <summary>
        /// GetElementEnumeratro.
        /// </summary>
        /// <returns>IDictionaryEnumerator</returns>
        public IDictionaryEnumerator GetElementEnumerator()
        {
            return _elemetDic.GetEnumerator();
        }

        /// <summary>
        /// LocaltoWorld. Get world position from local position.
        /// </summary>
        /// <param name="localPosition">local position</param>
        /// <returns>world position</returns>
        public Vector3 LocalToWorld(Vector3 localPosition)
        {
            return _camera.Position + localPosition;
        }

        /// <summary>
        /// LocaltoWorld. Get world position from local position.
        /// </summary>
        /// <param name="localPosition">local position</param>
        /// <returns>world position</returns>
        public Vector2 LocalToWorld(Vector2 localPosition)
        {
            Vector3 localPos = new Vector3(localPosition.X, localPosition.Y, 0.0f);
            Vector3 worldPos = _camera.Position + localPos;
            return new Vector2(worldPos.X, worldPos.Y);
        }
    }
}
