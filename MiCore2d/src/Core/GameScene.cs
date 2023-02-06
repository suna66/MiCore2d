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
        private TextureManager _textureManager;

        private OrderedDictionary _elemetDic;

        private Camera? _camera = null;

        private GameControl? _control = null;

        private Canvas? _canvas = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public GameScene()
        {
            _elemetDic = new OrderedDictionary();

            _textureManager = new TextureManager();

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
        public virtual void OnUpdate(double elapsed)
        {
            CurrentTime += elapsed;
            Update(elapsed);
        }

        /// <summary>
        /// Update. Abstruct method. Called game control class every frame.
        /// </summary>
        /// <param name="elapsed">elapsed time</param>
        public abstract void Update(double elapsed);

        /// <summary>
        /// OnUnLoad
        /// </summary>
        public virtual void OnUnLoad()
        {
            _elemetDic.Clear();
            _textureManager.Clear();
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
                Element element = (Element)enumerator.Value;

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
            IDictionaryEnumerator enumerator = _elemetDic.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Element element = (Element)enumerator.Value;
                if (!element.Disabled && !element.Destroyed)
                {
                    element.DrawRenderer?.Draw(_camera, element);
                }
            }
            _canvas.Update();
        }

        /// <summary>
        /// LoadTexture2d. Loading 2D texture data from file.
        /// </summary>
        /// <param name="key">namagement name</param>
        /// <param name="filename">file name</param>
        /// <returns>Texture</returns>
        public Texture LoadTexture2d(string key, string filename)
        {
            Texture texture = _textureManager.LoadTexture2d(key, filename);
            return texture;
        }

        /// <summary>
        /// LoadTexture2dArray. Loading multiple 2D texture data from files.
        /// </summary>
        /// <param name="key">management name</param>
        /// <param name="files">image file list</param>
        /// <param name="width">widht of an image file</param>
        /// <param name="height">height of an image file</param>
        /// <returns>Texture</returns>
        public Texture LoadTexture2dArray(string key, string[] files, int width, int height)
        {
            Texture texture = _textureManager.LoadTexture2dArray(key, files, width, height);
            return texture;
        }

        /// <summary>
        /// LoadTexture2dTile. Loading tilemap texture data from file.
        /// </summary>
        /// <param name="key">management name</param>
        /// <param name="filename">image file name</param>
        /// <param name="tileW">one tile width</param>
        /// <param name="tileH">one tile height</param>
        /// <returns>Texture</returns>
        public Texture LoadTexture2dTile(string key, string filename, int tileW, int tileH)
        {
            Texture texture = _textureManager.LoadTexture2dTile(key, filename, tileW, tileH);
            return texture;
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
            element.SetParentGameScene(this);
            element.Name = key;
            element.Layer = layerName;
            _elemetDic.Add(key, element);
            return element;
        }

        /// <summary>
        /// GetTexture. Getting texture data from managed data.
        /// </summary>
        /// <param name="key">manangement name</param>
        /// <returns>Texture</returns>
        public Texture GetTexture(string key)
        {
            return _textureManager.GetTexture(key);
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
            return (Element)_elemetDic[key];
        }

        /// <summary>
        /// GetElementEnumeratro.
        /// </summary>
        /// <returns>IDictionaryEnumerator</returns>
        public IDictionaryEnumerator GetElementEnumerator()
        {
            return _elemetDic.GetEnumerator();
        }
    }
}
