using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    public class RendererManager
    {
        //private static RendererManager _instance = null!;

        private Dictionary<string, Renderer> _rendererDic;

        public RendererManager()
        {
            _rendererDic = new Dictionary<string, Renderer>();
        }

        // public static RendererManager GetInstance()
        // {
        //     if (_instance == null)
        //     {
        //         _instance = new RendererManager();
        //     }
        //     return _instance;
        // }

        public void Add(string key, Renderer renderer)
        {
            _rendererDic.Add(key, renderer);
        }

        public Renderer Get(string key)
        {
            return _rendererDic[key];
        }

        public bool HasKey(string key)
        {
            return _rendererDic.ContainsKey(key);
        }

        public int GetTextureCount()
        {
            return _rendererDic.Count;
        }

        public void Remove(string key)
        {
            if (HasKey(key))
            {
                _rendererDic[key].Dispose();
                _rendererDic.Remove(key);
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, Renderer> kvp in _rendererDic)
            {
                Renderer renderer = kvp.Value;
                renderer.Dispose();
            }
            _rendererDic.Clear();
        }

        public void Dispose()
        {
            Clear();
            GC.SuppressFinalize(this);
        }
    }
}