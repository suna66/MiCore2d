using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class RendererManager
    {
        private static RendererManager? _instance = null;
        private List<Renderer> _rendererList;

        public static RendererManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RendererManager();
                _instance.AddRenderer<TextureRenderer>();
                _instance.AddRenderer<SepiaTextureRenderer>();
                _instance.AddRenderer<TextureArrayRenderer>();
                _instance.AddRenderer<LineRenderer>();
                _instance.AddRenderer<PolygonRenderer>();
            }
            return _instance;
        }

        private RendererManager()
        {
            _rendererList = new List<Renderer>();
        }

        public T AddRenderer<T>() where T : new()
        {
            T obj = new T();
            if (obj is Renderer)
            {
                Renderer renderer = (Renderer)(object)obj;
                _rendererList.Add(renderer);
                return obj;
            }
            throw new InvalidCastException("cannot convert to Renderer Object");
        }

        public T GetRenderer<T>()
        {
            foreach(Renderer c in _rendererList)
            {
                if (c is T)
                {
                    return (T)(object)c;
                }
            }
            return (T)(object)null!;
        }

        public int GetCount()
        {
            return _rendererList.Count;
        }

        public void Clear(bool renew)
        {
            foreach (Renderer renderer in _rendererList)
            {
                renderer.Dispose();
            }
            if (renew)
                _rendererList = new List<Renderer>();
            else
                _rendererList = null!;
        }

        public void Dispose()
        {
            Clear(false);
            GC.SuppressFinalize(this);
        }
    }
}