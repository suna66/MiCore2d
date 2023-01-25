using System;

namespace MiCore2d
{
    public class ShaderManager
    {
        private static ShaderManager _instance = null!;

        private Dictionary<string, Shader> _shaderList;

        private ShaderManager()
        {
            _shaderList = new Dictionary<string, Shader>();
        }

        public static ShaderManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ShaderManager();
            }
            return _instance;
        }

        public void AddShader(Shader shader, string name)
        {
            if (shader == null)
            {
                throw new ArgumentException("shader instance at parameter is not correct");
            }
            if (name == null || name.Length == 0)
            {
                throw new ArgumentException("shader name is not set");
            }
            //check duplication
            if (_shaderList[name] != null)
            {
                Shader s = _shaderList[name];
                s.Dispose();
            }
            _shaderList.Add(name, shader);
        }

        public void AddShaderFromFile(string vertFile, string fragFile, string name)
        {
            if (vertFile == null || vertFile.Length == 0)
            {
                throw new ArgumentException("vert file name is not specfied");
            }
            if (fragFile == null || fragFile.Length == 0)
            {
                throw new ArgumentException("frag file name is not specfied");
            }
            if (name == null || name.Length == 0)
            {
                throw new ArgumentException("shader name is not set");
            }
            //check duplication
            if (_shaderList[name] != null)
            {
                Shader s = _shaderList[name];
                s.Dispose();
            }
            Shader shader = new Shader(vertFile, fragFile);
            _shaderList.Add(name, shader);
        }
    }
}