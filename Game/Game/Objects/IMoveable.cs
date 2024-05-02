using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IMoveable
    {
        // я умею двигаться к обьекту
        // Стремление к точке
        // Но в данном случае стремление к сектору в сетке
        void MoveInOneFrame(GameObject gameObject);
    }
}
