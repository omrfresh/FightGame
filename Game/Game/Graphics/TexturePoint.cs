using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class TexturePoint
    {
        public double S { get; set; } // координата текстуры X
        public double T { get; set; } // координата текстуры Y
        public TexturePoint()
        {

        }
        public TexturePoint(double s, double t)
        {
            S = s;
            T = t;
        }

        public void NewPoint(double s, double t)
        {
            S = s;
            T = t;
        }

        public static TexturePoint[] Default()
        {
            TexturePoint[] texturePoints = new TexturePoint[4];
            texturePoints[0] = new TexturePoint(0, 1);
            texturePoints[1] = new TexturePoint(1, 1);
            texturePoints[2] = new TexturePoint(1, 0);
            texturePoints[3] = new TexturePoint(0, 0);

            return texturePoints;
        }
        public double[] ToArray()
        {
            return new double[] { S, T };
        }
    }
}
