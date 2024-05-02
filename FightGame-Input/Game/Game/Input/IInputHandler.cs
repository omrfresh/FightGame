using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Input
{
    public interface IInputHandler
    {
        void HandleInput(KeyboardState keyboardState);
    }

}
