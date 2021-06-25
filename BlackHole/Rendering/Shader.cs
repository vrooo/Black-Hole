using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.IO;
using System.Text;

namespace BlackHole
{
    public class Shader
    {
        private int program;

        public Shader(string vertPath, string fragPath)
        {
            program = GL.CreateProgram();

            int vert = CompileShader(vertPath, ShaderType.VertexShader);
            GL.AttachShader(program, vert);
            int frag = CompileShader(fragPath, ShaderType.FragmentShader);
            GL.AttachShader(program, frag);

            GL.LinkProgram(program);
            GL.DetachShader(program, vert);
            GL.DeleteShader(vert);
            GL.DetachShader(program, frag);
            GL.DeleteShader(frag);
        }

        public void Use()
        {
            GL.UseProgram(program);
        }

        public void BindMatrix(Matrix4 matrix, string name)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.UniformMatrix4(location, false, ref matrix);
        }

        public void BindVector4(Vector4 vector, string name)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform4(location, vector);
        }

        public void BindInt(int obj, string name)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform1(location, obj);
        }

        public void BindFloat(float obj, string name)
        {
            int location = GL.GetUniformLocation(program, name);
            GL.Uniform1(location, obj);
        }

        private int CompileShader(string path, ShaderType type)
        {
            string source;
            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                source = sr.ReadToEnd();
            }

            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);
            return shader;
        }
    }
}
