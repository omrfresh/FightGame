using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class MapGenerate
    {
        private (int width, int height) _sides;
        private (double Width, double Up) _offsets;
        public (double X, double Y) units;
        public RectangleWithTexture[,] RectangleWithTextures { get; private set; }
        public MapGenerate(int width, int heidth, double offsetWidth, double offsetUp, double offsetDown)
        {
            _sides.width = width;
            _sides.height = heidth;
            units.X = (double) (2 - 2 * offsetWidth) / width;
            units.Y = (double) (2 - (offsetUp + offsetDown)) / heidth;
            _offsets.Width = offsetWidth;
            _offsets.Up = offsetUp;
            RectangleWithTextures = new RectangleWithTexture[_sides.height, _sides.width];
            GeneratePoints();
        }
        public void GeneratePoints()
        {
            double x = -1.0 + _offsets.Width;
            double y =  1.0 - _offsets.Up;
            double z = 0.0;
            for (int i = 0; i < RectangleWithTextures.GetLength(0); i++)
            {
                if (i != 0)
                {
                    y -= units.Y;
                }
                x = -1.0 + _offsets.Width;
                for (int j = 0; j < RectangleWithTextures.GetLength(1); j++)
                {
                    RectangleWithTextures[i, j] =
                    (
                        new RectangleWithTexture
                        (
                            new Rectangle
                            (
                                new Point(x, y, z),
                                new Point(x + units.X, y, z),
                                new Point(x + units.X, y - units.Y, z),
                                new Point(x, y - units.Y, z)
                            ),
                            [new TexturePoint(1.0, 1.0), new TexturePoint(1.0, 0.0), new TexturePoint(0.0, 0.0), new TexturePoint(0.0, 1.0)]
                        )
                    );
                    x += units.X;
                }
            }
        }
    }
}
