using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MiCore2d
{
    /// <summary>
    /// Shader.
    /// </summary>
    public class Shader : IDisposable
    {
        /// <summary>
        /// Handle.
        /// </summary>
        public readonly int Handle;

        private readonly Dictionary<string, int> _uniformLocations;

        private bool _disposed = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertString">glsl string for vertics</param>
        /// <param name="fragString">glsl string for fragment</param>
        public Shader(string vertString, string fragString)
        {
            string shaderSource = vertString; //File.ReadAllText(vertPath);
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderSource);
            CompileShader(vertexShader);

            shaderSource = fragString; //File.ReadAllText(fragPath);
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            LinkProgram(Handle);

            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            int numberOfUniforms;
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out numberOfUniforms);

            _uniformLocations = new Dictionary<string, int>();

            for (int i = 0; i < numberOfUniforms; i++)
            {
                string key = GL.GetActiveUniform(Handle, i, out _, out _);

                int location = GL.GetUniformLocation(Handle, key);

                _uniformLocations.Add(key, location);
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    GL.DeleteProgram(Handle);
                }
                _disposed = true;
            }
        }

        /// <summary>
        ///  Destructor.
        /// </summary>
        ~Shader()
        {
            Dispose(false);
        }

        /// <summary>
        /// CompileShader.
        /// </summary>
        /// <param name="shader">shader id</param>
        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);

            int success;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        /// <summary>
        /// LinkProgram.
        /// </summary>
        /// <param name="program">program id</param>
        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);
            int success;
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
            {
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        /// <summary>
        /// Use.
        /// </summary>
        public void Use()
        {
            GL.UseProgram(Handle);
        }

        /// <summary>
        /// GetAttribLocation.
        /// </summary>
        /// <param name="attribName">attribute name</param>
        /// <returns>location no</returns>
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(Handle, attribName);
        }

        /// <summary>
        /// SetInt
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="data">value</param>
        public void SetInt(string name, int data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        /// <summary>
        /// SetFloat
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="data">value</param>
        public void SetFloat(string name, float data)
        {
            GL.UseProgram(Handle);
            GL.Uniform1(_uniformLocations[name], data);
        }

        /// <summary>
        /// SetMatrix4
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="data">value</param>
        public void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(_uniformLocations[name], true, ref data);
        }

        /// <summary>
        /// SetVector3
        /// </summary>
        /// <param name="name">attribute name</param>
        /// <param name="data">value</param>
        public void SetVector3(string name, Vector3 data)
        {
            GL.UseProgram(Handle);
            GL.Uniform3(_uniformLocations[name], data);
        }

    }
}
