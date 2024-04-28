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

namespace Game
{
    public class Buffer
    {
        int vertexArrayObject = 0;
        int vertexBufferObject = 0;
        public double[] vertices;
        static Shader shader;

        static Buffer()
        {
            shader = new Shader(@"Shaders\shader.vert", @"Shaders\shader.frag");
        }
        public Buffer(double[] vertices)
        {
            this.vertices = vertices;
            GenerateBuffers();
        }

        private void GenerateBuffers()
        {
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(double), vertices, BufferUsageHint.DynamicDraw);

            shader = new Shader(@"Shaders\shader.vert", @"Shaders\shader.frag");
            shader.Use();
            var vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Double, false, 5 * sizeof(double), 0);

            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Double, false, 5 * sizeof(double), 3 * sizeof(double));
        }

        public void UpdateDate(double[] vertices)
        {
            this.vertices = vertices;
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, this.vertices.Length * sizeof(double), this.vertices, BufferUsageHint.DynamicDraw);
        }

        public void Render(Texture texture)
        {
            GL.BindVertexArray(vertexArrayObject);
            texture.Use(TextureUnit.Texture0);
            shader.Use();
            GL.DrawArrays(PrimitiveType.Triangles, 0, vertices.Length / 5);
        }

        public void Dispose()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vertexBufferObject);
            vertexBufferObject = 0;

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(vertexArrayObject);
            vertexArrayObject = 0;
        }
    }
}
