using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal interface IGameCore
    {
        (int x, int y) Index { get; set; }
        bool IsSolid { get; set; }
    }
}
