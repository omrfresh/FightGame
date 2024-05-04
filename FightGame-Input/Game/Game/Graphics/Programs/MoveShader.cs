using System;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace Game
{
    public class MoveShader : Shader
    {
        private int _positionLocation;

        public MoveShader(string vertPath, string fragPath) : base(vertPath, fragPath)
        {
            _positionLocation = GL.GetUniformLocation(Handle, "u_Position");
        }

        public void SetPosition(Vector2 position)
        {
            GL.UseProgram(Handle);
            GL.Uniform2(_positionLocation, position);
        }
    }
}
