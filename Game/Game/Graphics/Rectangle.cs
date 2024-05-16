using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    // Класс для представления прямоугольника
    public class Rectangle
    {
        // Верхняя левая точка прямоугольника
        public Point TopLeft { get; set; }
        // Верхняя правая точка прямоугольника
        public Point TopRight { get; set; }
        // Нижняя правая точка прямоугольника
        public Point BottomRight { get; set; }
        // Нижняя левая точка прямоугольника
        public Point BottomLeft { get; set; }
        // Массив всех точек прямоугольника
        public Point[] Points { get; set; }

        // Конструктор прямоугольника с заданными точками
        public Rectangle(Point topLeft, Point topRight, Point bottomRight, Point bottomLeft)
        {
            SetPnts(topLeft, topRight, bottomRight, bottomLeft);
        }

        // Конструктор прямоугольника с заданной верхней левой точкой, шириной и высотой
        public Rectangle(Point topLeft, double width, double height)
        {
            SetPnts(topLeft, width, height);
        }

        // Конструктор по умолчанию
        public Rectangle()
        {
        }

        // Установка точек прямоугольника
        public void SetPnts(Point topLeft, Point topRight, Point bottomRight, Point bottomLeft)
        {
            Points = new Point[4];
            Points[0] = TopLeft = topLeft;
            Points[1] = TopRight = topRight;
            Points[2] = BottomRight = bottomRight;
            Points[3] = BottomLeft = bottomLeft;
        }

        // Установка точек прямоугольника по верхней левой точке, ширине и высоте
        public void SetPnts(Point topLeft, double width, double height)
        {
            Points = new Point[4];

            Points[0] = TopLeft = topLeft;

            Points[1] = TopRight = new Point
                (
                    topLeft.X + width,
                    topLeft.Y,
                    topLeft.Z
                );

            Points[2] = BottomRight = new Point
                (
                    topLeft.X + width,
                    topLeft.Y - height,
                    topLeft.Z
                );
            Points[3] = BottomLeft = new Point
                (
                    topLeft.X,
                    topLeft.Y - height,
                    topLeft.Z
                );
        }

        // Получить ширину объекта
        public double GetWidth()
        {
            return Math.Abs(TopRight.X - TopLeft.X);
        }

        // Получить высоту объекта
        public double GetHeight()
        {
            return Math.Abs(TopLeft.Y - BottomLeft.Y);
        }

        // Преобразовать координаты точек прямоугольника в массив
        public double[] ToArray()
        {
            return Points[0].ToArray().Concat(Points[1].ToArray()).ToArray().Concat(Points[2].ToArray()).ToArray().Concat(Points[3].ToArray()).ToArray();
        }

        // Проверить, пересекаются ли данный прямоугольник и другой прямоугольник
        public bool Intersects(Rectangle other)
        {
            // Погрешность границ объекта, устанавливается для более корректной работы колизий объектов
            double correctNum = 0.0001;

            return !((other.TopLeft.X + other.GetWidth() * correctNum) > TopRight.X ||
                     (other.TopRight.X - other.GetWidth() * correctNum) < TopLeft.X ||
                     (other.TopLeft.Y - other.GetHeight() * correctNum) < BottomLeft.Y ||
                     (other.BottomLeft.Y + other.GetHeight() * correctNum) > TopLeft.Y);
        }

        // Сдвинуть прямоугольник по оси X на заданное расстояние
        public void MoveX(double X)
        {
            foreach (var point in Points)
            {
                point.X += X;
            }
        }

        // Сдвинуть прямоугольник по оси Y на заданное расстояние
        public void MoveY(double Y)
        {
            foreach (var point in Points)
            {
                point.Y += Y;
            }
        }
    }

}
