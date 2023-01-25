using System;

namespace MiCore2d
{
    public class TextureManager
    {
        private Dictionary<string, Texture> _textureDic;

        public TextureManager()
        {
            _textureDic = new Dictionary<string, Texture>();
        }

        public Texture LoadTexture2d(string key, string path)
        {
            if (_textureDic.ContainsKey(key))
            {
                return _textureDic[key];
            }
            Texture2d texture = new Texture2d(path);
            _textureDic.Add(key, texture);
            return texture;
        }

        public Texture LoadTexture2dArray(string key, string[] files, int width, int height)
        {
            if (_textureDic.ContainsKey(key))
            {
                return _textureDic[key];
            }
            Texture2dArray texture = new Texture2dArray(files, width, height);
            _textureDic.Add(key, texture);
            return texture;
        }

        public Texture LoadTexture2dTile(string key, string path, int tileW, int tileH)
        {
            if (_textureDic.ContainsKey(key))
            {
                return _textureDic[key];
            }
            Texture2dTile texture = new Texture2dTile(path, tileW, tileH);
            _textureDic.Add(key, texture);
            return texture;
        }

        public Texture GetTexture(string key)
        {
            return _textureDic[key];
        }

        public bool HasKey(string key)
        {
            return _textureDic.ContainsKey(key);
        }

        public int GetTextureCount()
        {
            return _textureDic.Count;
        }

        public void Remove(string key)
        {
            if (HasKey(key))
            {
                _textureDic[key].Dispose();
                _textureDic.Remove(key);
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, Texture> kvp in _textureDic)
            {
                Texture tex = kvp.Value;
                tex.Dispose();
            }
            _textureDic.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}