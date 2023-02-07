using System;

namespace MiCore2d
{
    /// <summary>
    /// TextureManager.
    /// </summary>
    public class TextureManager
    {
        private Dictionary<string, Texture> _textureDic;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TextureManager()
        {
            _textureDic = new Dictionary<string, Texture>();
        }

        /// <summary>
        /// LoadTexture2d.
        /// </summary>
        /// <param name="key">management name</param>
        /// <param name="path">file path</param>
        /// <returns>texture</returns>
        public Texture2d LoadTexture2d(string key, string path)
        {
            if (_textureDic.ContainsKey(key))
            {
                return (Texture2d)_textureDic[key];
            }
            Texture2d texture = new Texture2d(path);
            _textureDic.Add(key, texture);
            return texture;
        }

        /// <summary>
        /// LoadTexture2d
        /// </summary>
        /// <param name="key">management name</param>
        /// <param name="stream">stream</param>
        /// <returns>texture</returns>
        public Texture2d LoadTexture2d(string key, Stream stream)
        {
            if (_textureDic.ContainsKey(key))
            {
                return (Texture2d)_textureDic[key];
            }
            Texture2d texture = new Texture2d(stream);
            _textureDic.Add(key, texture);
            return texture;
        }

        /// <summary>
        /// LoadTexture2dArray.
        /// </summary>
        /// <param name="key">management name</param>
        /// <param name="files">file list</param>
        /// <param name="width">width of an image file</param>
        /// <param name="height">height of an image file</param>
        /// <returns>texture</returns>
        public Texture2dArray LoadTexture2dArray(string key, string[] files, int width, int height)
        {
            if (_textureDic.ContainsKey(key))
            {
                return (Texture2dArray)_textureDic[key];
            }
            Texture2dArray texture = new Texture2dArray(files, width, height);
            _textureDic.Add(key, texture);
            return texture;
        }

        /// <summary>
        /// LoadTexture2dArray.
        /// </summary>
        /// <param name="key">management name</param>
        /// <param name="array_size">array size</param>
        /// <param name="width">width of an image file</param>
        /// <param name="height">height of an image file</param>
        /// <returns>texture</returns>
        public Texture2dArray LoadTexture2dArray(string key, int array_size, int width, int height)
        {
            if (_textureDic.ContainsKey(key))
            {
                return (Texture2dArray)_textureDic[key];
            }
            Texture2dArray texture = new Texture2dArray(array_size, width, height);
            _textureDic.Add(key, texture);
            return texture;
        }

        /// <summary>
        /// LoadTexture2dTile.
        /// </summary>
        /// <param name="key">management name</param>
        /// <param name="path">file path</param>
        /// <param name="tileW">tile width</param>
        /// <param name="tileH">tile height</param>
        /// <returns></returns>
        public Texture2dTile LoadTexture2dTile(string key, string path, int tileW, int tileH)
        {
            if (_textureDic.ContainsKey(key))
            {
                return (Texture2dTile)_textureDic[key];
            }
            Texture2dTile texture = new Texture2dTile(path, tileW, tileH);
            _textureDic.Add(key, texture);
            return texture;
        }

        /// <summary>
        /// LoadTexture2dTile.
        /// </summary>
        /// <param name="key">management name</param>
        /// <param name="stream">stream</param>
        /// <param name="tileW">tile width</param>
        /// <param name="tileH">tile height</param>
        /// <returns></returns>
        public Texture2dTile LoadTexture2dTile(string key, Stream stream, int tileW, int tileH)
        {
            if (_textureDic.ContainsKey(key))
            {
                return (Texture2dTile)_textureDic[key];
            }
            Texture2dTile texture = new Texture2dTile(stream, tileW, tileH);
            _textureDic.Add(key, texture);
            return texture;
        }

        /// <summary>
        /// GetTexture.
        /// </summary>
        /// <param name="key">management name</param>
        /// <returns>texture</returns>
        public Texture GetTexture(string key)
        {
            return _textureDic[key];
        }

        /// <summary>
        /// Haskey.
        /// </summary>
        /// <param name="key">management name</param>
        /// <returns>true: there is a key</returns>
        public bool HasKey(string key)
        {
            return _textureDic.ContainsKey(key);
        }

        /// <summary>
        /// GetTextureCount.
        /// </summary>
        /// <returns>number of managed textures.</returns>
        public int GetTextureCount()
        {
            return _textureDic.Count;
        }

        /// <summary>
        /// Remove.
        /// </summary>
        /// <param name="key">management name</param>
        public void Remove(string key)
        {
            if (HasKey(key))
            {
                _textureDic[key].Dispose();
                _textureDic.Remove(key);
            }
        }

        /// <summary>
        /// Clear.
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<string, Texture> kvp in _textureDic)
            {
                Texture tex = kvp.Value;
                tex.Dispose();
            }
            _textureDic.Clear();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Clear();
        }
    }
}