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
    public abstract class GameScene
    {
        private TextureManager _textureManager;

        private OrderedDictionary _elemetDic;

        private Camera? _camera = null;

        private RendererManager _rendererManager;

        private GameControl? _control = null;

        private Canvas? _canvas = null;

        public GameScene()
        {
            _elemetDic = new OrderedDictionary();

            _textureManager = new TextureManager();

            _rendererManager = new RendererManager();

            CurrentTime = 0;
        }

        public void Init(GameControl control)
        {
            _control = control;
            
            _rendererManager.Add("sprite", new TextureRenderer());
            _rendererManager.Add("sepia", new SepiaTextureRenderer());
            _rendererManager.Add("array", new TextureArrayRenderer());
            _rendererManager.Add("line", new LineRenderer());
            _rendererManager.Add("rect", new PolygonRenderer());

            _camera = new Camera(Vector3.UnitZ * _control.UnitCount, GW.Size.X / (float)GW.Size.Y);
            _camera.CameraType = CAMERA_TYPE.ORTHONGRAPHIC;

            _canvas = new Canvas(GW.Size.X, GW.Size.Y);
        }

        public GameWindow GW { get => _control.GW; }

        public KeyboardState KeyState { get => _control.GW.KeyboardState; }

        public AudioSource Audio { get => _control.Audio; }

        public GameControl Control { get => _control; }

        public Canvas SceneCanvas { get => _canvas; }

        public Camera SceneCamera { get => _camera; }

        public double CurrentTime { get; set; }

        public virtual void OnLoad()
        {
            Load();
        }

        public abstract void Load();

        public virtual void OnUpdate(double elapsed)
        {
            CurrentTime += elapsed;
            Update(elapsed);
        }

        public abstract void Update(double elapsed);

        public virtual void OnUnLoad()
        {
            _elemetDic.Clear();
            _textureManager.Clear();
            _rendererManager.Dispose();
            if (_canvas != null)
            {
                _canvas.Dispose();
            }
        }

        public virtual void OnAfterUpdate(double elapsed)
        {
            bool hasDestory = false;
            string deleteKey = string.Empty;
            IDictionaryEnumerator enumerator = _elemetDic.GetEnumerator();
            //foreach (KeyValuePair<string, Element> kvp in _elemetDic)
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

        public virtual void OnResize(ResizeEventArgs e)
        {
            _camera.AspectRatio = GW.Size.X / (float)GW.Size.Y;
        }

        public virtual void OnMouseButton(MouseButton button, bool pressed)
        {

        }

        public virtual void OnMouseMove(float x, float y, float deltaX, float deltaY)
        {

        }

        public virtual void OnMouseWheel(float offsetX, float offsetY)
        {

        }

        public virtual void OnRenderer(double elapsed)
        {
            IDictionaryEnumerator enumerator = _elemetDic.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Element element = (Element)enumerator.Value;
                // if (element.Texture != null)
                // {
                //     if (element.Visible && !element.Destroyed)
                //         _rendererManager.Get(element.RendererName).Draw(_camera, element);
                // }
                if (element.Visible && !element.Destroyed)
                         _rendererManager.Get(element.RendererName).Draw(_camera, element);
            }
            _canvas.Update();
        }

        public Texture LoadTexture2d(string key, string filename)
        {
            Texture texture = _textureManager.LoadTexture2d(key, filename);
            return texture;
        }

        public Texture LoadTexture2dArray(string key, string[] files, int width, int height)
        {
            Texture texture = _textureManager.LoadTexture2dArray(key, files, width, height);
            return texture;
        }

        public Texture LoadTexture2dTile(string key, string filename, int tileW, int tileH)
        {
            Texture texture = _textureManager.LoadTexture2dTile(key, filename, tileW, tileH);
            return texture;
        }

        // public Element AddBasicSprite(string key, string textureName, float unit, string layerName = "default")
        // {
        //     Texture texture = _textureManager.GetTexture(textureName);
        //     if (texture is not Texture2d)
        //     {
        //         throw new InvalidCastException($"{textureName} texture is not Texture2d");
        //     }
        //     BasicSprite sprite = new BasicSprite(texture, unit);
        //     sprite.SetParentGameScene(this);
        //     sprite.Name = key;
        //     sprite.Layer = layerName;
        //     _elemetDic.Add(key, sprite);
        //     return sprite;
        // }

        // public Element AddAnimationSprite(string key, string textureName, float unit, string layerName = "default")
        // {
        //     Texture texture = _textureManager.GetTexture(textureName);
        //     if (texture is not Texture2dArray && texture is not Texture2dTile)
        //     {
        //         throw new InvalidCastException($"{textureName} texture is not Texture2d or Texture2dTile");
        //     }
        //     AnimationSprite sprite = new AnimationSprite(texture, unit);
        //     sprite.SetParentGameScene(this);
        //     sprite.Name = key;
        //     sprite.Layer = layerName;
        //     _elemetDic.Add(key, sprite);
        //     return sprite;
        // }

        public Element AddImageSprite(string key, string textureName, float unit, string layerName = "default")
        {
            string rendererName = "sprite";
            Texture texture = _textureManager.GetTexture(textureName);
            if (texture is Texture2dArray || texture is Texture2dTile)
            {
                rendererName = "array";
            }
            ImageSprite sprite = new ImageSprite(texture, unit);
            sprite.SetParentGameScene(this);
            sprite.Name = key;
            sprite.Layer = layerName;
            sprite.RendererName = rendererName;
            _elemetDic.Add(key, sprite);
            return sprite;
        }

        public Element AddPlainSprite(string key, float unit, string layerName = "default")
        {
            PlainSprite sprite = new PlainSprite(unit);
            sprite.SetParentGameScene(this);
            sprite.Name = key;
            sprite.Layer = layerName;
            _elemetDic.Add(key, sprite);
            return sprite;
        }

        public Element AddTilemapSprite(string key, string textureName, float unit, float[] mapdata, bool is_dynamic, string layerName = "default")
        {
            Texture texture = _textureManager.GetTexture(textureName);
            if (texture is not Texture2dTile)
            {
                throw new InvalidCastException($"{textureName} texture is not  Texture2dTile");
            }
            _rendererManager.Add(key, new InstancedRenderer(mapdata, is_dynamic));
            TilemapSprite sprite = new TilemapSprite(texture, unit, key);
            sprite.SetParentGameScene(this);
            sprite.Name = key;
            sprite.RendererName = key;
            sprite.Layer = layerName;
            sprite.SetPositionMap(mapdata);
            _elemetDic.Add(key, sprite);
            return sprite;
        }

        public Element AddElement(string key, Element element, string layerName = "default")
        {
            element.SetParentGameScene(this);
            element.Name = key;
            element.Layer = layerName;
            _elemetDic.Add(key, element);
            return element;
        }

        public Texture GetTexture(string key)
        {
            return _textureManager.GetTexture(key);
        }

        public Element GetElement(string key)
        {
            if (!_elemetDic.Contains(key))
            {
                throw new KeyNotFoundException(key);
            }
            return (Element)_elemetDic[key];
        }

        public IDictionaryEnumerator GetElementEnumerator()
        {
            return _elemetDic.GetEnumerator();
        }
    }
}
