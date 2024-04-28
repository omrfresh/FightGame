using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Point()
        {

        }
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public void NewPoint(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double[] ToArray()
        {
            return new double[] { X, Y, Z };
        }

        public object Clone()
        {
            return new Point(X, Y, Z);
        }
    }
}
