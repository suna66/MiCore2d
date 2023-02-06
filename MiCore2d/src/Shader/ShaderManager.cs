using System;

namespace MiCore2d
{
    /// <summary>
    /// ShaderManager
    /// </summary>
    public class ShaderManager
    {
        private static ShaderManager _instance = null!;

        private Dictionary<string, Shader> _shaderList;

        /// <summary>
        /// Constructor.
        /// </summary>
        private ShaderManager()
        {
            _shaderList = new Dictionary<string, Shader>();
        }

        /// <summary>
        /// GetInstance.
        /// </summary>
        /// <returns>instance</returns>
        public static ShaderManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ShaderManager();
            }
            return _instance;
        }

        /// <summary>
        /// AddShader.
        /// </summary>
        /// <param name="shader">shader instance</param>
        /// <param name="name">managment name</param>
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

        /// <summary>
        /// AddShaderFromFile.
        /// </summary>
        /// <param name="vertFile">glsl vertics file</param>
        /// <param name="fragFile">glsl fragment file</param>
        /// <param name="name">management name</param>
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