using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;

namespace Game
{
    public class BoundingBox
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public BoundingBox(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public bool IsCollidingWith(BoundingBox other)
        {
            // Проверка на пересечение прямоугольников
            return !(Position.X + Size.X < other.Position.X ||
                     Position.X > other.Position.X + other.Size.X ||
                     Position.Y + Size.Y < other.Position.Y ||
                     Position.Y > other.Position.Y + other.Size.Y);
        }
    }
}
