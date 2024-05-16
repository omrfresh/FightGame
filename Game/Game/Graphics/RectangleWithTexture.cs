using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Game
{
    // Класс для представления прямоугольника с текстурными координатами
    public class RectangleWithTexture
    {
        // Прямоугольник, определяющий геометрические границы объекта
        [XmlElement(ElementName = nameof(Game.Rectangle))]
        public Rectangle Rectangle { get; set; }

        // Массив текстурных координат для каждой вершины прямоугольника
        [XmlElement(ElementName = nameof(TexturePoint))]
        public TexturePoint[] TexturePoints { get; set; }

        // Конструктор прямоугольника с текстурными координатами по заданному прямоугольнику и массиву текстурных координат
        public RectangleWithTexture(Rectangle rectangle, TexturePoint[] texturePoints)
        {
            this.Rectangle = rectangle;
            this.TexturePoints = texturePoints;
        }

        // Конструктор по умолчанию
        public RectangleWithTexture()
        {
        }

        // Конструктор прямоугольника с текстурными координатами по заданным вершинам прямоугольника и массиву текстурных координат
        public RectangleWithTexture(Point TopR, Point TopL, Point BotL, Point BotR, TexturePoint[] texturePoints)
        {
            Rectangle = new Rectangle(TopL, TopR, BotL, BotR);
            this.TexturePoints = texturePoints;
        }

        // Преобразовать координаты вершин прямоугольника и текстурные координаты в массив
        public double[] ToArray()
        {
            return Rectangle.TopLeft.ToArray()
                .Concat(TexturePoints[0].ToArray()).ToArray()
                .Concat(Rectangle.TopRight.ToArray()).ToArray()
                .Concat(TexturePoints[1].ToArray()).ToArray()
                .Concat(Rectangle.BottomRight.ToArray()).ToArray()
                .Concat(TexturePoints[2].ToArray()).ToArray()
                .Concat(Rectangle.BottomLeft.ToArray()).ToArray()
                .Concat(TexturePoints[3].ToArray()).ToArray();
        }
    }

}
