using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IObjectCore
    {
        string Name { get; set; }
        double[] Vertices { get; set; }
        RectangleWithTexture RectangleWithTexture { get; set; }
        Texture Texture { get; set; }
        Buffer Buffer { get; set; }
    }
}
