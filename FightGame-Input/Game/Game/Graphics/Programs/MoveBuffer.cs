using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Numerics;
using SixLabors.ImageSharp.Memory;

namespace Game
{
    public class MoveBuffer : IDisposable
    {
        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private MoveShader _shader;

        public MoveBuffer(double[] vertices, MoveShader shader)
        {
            _shader = shader;

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(double), vertices, BufferUsageHint.DynamicDraw);

            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Double, false, 5 * sizeof(double), 0);

            var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Double, false, 5 * sizeof(double), 3 * sizeof(double));
        }

        public void Render(Texture texture, OpenTK.Mathematics.Vector2 position)
        {
            _shader.Use();
            _shader.SetPosition(position);

            GL.BindVertexArray(_vertexArrayObject);
            texture.Use(TextureUnit.Texture0);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
        }
    }
}
