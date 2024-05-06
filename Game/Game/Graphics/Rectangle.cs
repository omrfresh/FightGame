using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Rectangle
    {
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }
        public Point BottomRight { get; set; }
        public Point BottomLeft { get; set; }
        public Point[] Points { get; set; }
        public Rectangle(Point topLeft, Point topRight, Point bottomRight, Point bottomLeft)
        {
            SetPnts(topLeft, topRight, bottomRight, bottomLeft);
        }
        public Rectangle(Point topLeft, double width, double hidth)
        {
            SetPnts(topLeft, width, hidth);
        }
        public Rectangle()
        {

        }
        public void SetPnts(Point topLeft, Point topRight, Point bottomRight, Point bottomLeft)
        {
            Points = new Point[4];
            Points[0] = TopLeft = topLeft;
            Points[1] = TopRight = topRight;
            Points[2] = BottomRight = bottomRight;
            Points[3] = BottomLeft = bottomLeft;
        }

        public void SetPnts(Point topLeft, double width, double hidth)
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
                    topLeft.Y - hidth,
                    topLeft.Z
                );
            Points[3] = BottomLeft = new Point
                (
                    topLeft.X,
                    topLeft.Y - hidth,
                    topLeft.Z
                );
        }
        /// <summary>
        /// Получить ширину объекта
        /// </summary>
        /// <returns></returns>
        public double GetWidth()
        {
            return Math.Abs(TopRight.X - TopLeft.X);
        }
        /// <summary>
        /// Получить высоту объекта
        /// </summary>
        /// <returns></returns>
        public double GetHeight()
        {
            return Math.Abs(TopLeft.Y - BottomLeft.Y);
        }

        public double[] ToArray()
        {
            return Points[0].ToArray().Concat(Points[1].ToArray()).ToArray().Concat(Points[2].ToArray()).ToArray().Concat(Points[3].ToArray()).ToArray();
        }
        public bool Intersects(Rectangle other)
        {
            // Погрешность границ объекта, устанавливается для более корректной работы колизий объектов
            double correctNum = 0.0001;

            return !((other.TopLeft.X + other.GetWidth() * correctNum) > TopRight.X ||
                     (other.TopRight.X - other.GetWidth() * correctNum) < TopLeft.X ||
                     (other.TopLeft.Y - other.GetHeight() * correctNum) < BottomLeft.Y ||
                     (other.BottomLeft.Y + other.GetHeight() * correctNum) > TopLeft.Y);
        }

        public void MoveX(double X)
        {
            foreach (var point in Points)
            {
                point.X += X;
            }
        }

        public void MoveY(double Y)
        {
            foreach (var point in Points)
            {
                point.Y += Y;
            }
        }
    }
}
