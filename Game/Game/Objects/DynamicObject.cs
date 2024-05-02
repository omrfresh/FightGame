using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class DynamicObject : GameObject, IMoveable
    {
        public int Time { get; private set; }
        public int FrameTime { get; private set; }
        private (double x, double y) _unit = (0, 0);

        protected DynamicObject(RectangleWithTexture rectangleWithTexture, Texture texture, (int x, int y) index) : base(rectangleWithTexture, texture, index)
        {
        }

        public void SetSpeed(int frameTime)
        {
            double x = RectangleWithTexture.Rectangle.GetWidth();
            double y = RectangleWithTexture.Rectangle.GetHeight();
            FrameTime = Time = frameTime;
            _unit = (x / frameTime, y / frameTime);
            IsSolid = true;
        }

        public void MoveInOneFrame(GameObject gameObject)
        {
            Time--;
            double errrorRate = 0.0001;
            if (Math.Abs(gameObject.RectangleWithTexture.Rectangle.TopLeft.X - RectangleWithTexture.Rectangle.TopLeft.X) < errrorRate)
            {
                // Для более точного фиксированного расположения игрока на позиции, иначе позиция обновляется в каждом кадре
            }
            else if (gameObject.RectangleWithTexture.Rectangle.TopLeft.X < RectangleWithTexture.Rectangle.TopLeft.X)
                RectangleWithTexture.Rectangle = new
                    (
                        new Point
                        (
                            RectangleWithTexture.Rectangle.TopLeft.X - _unit.x,
                            RectangleWithTexture.Rectangle.TopLeft.Y,
                            RectangleWithTexture.Rectangle.TopLeft.Z
                        ),
                        RectangleWithTexture.Rectangle.GetWidth(),
                        RectangleWithTexture.Rectangle.GetHeight()
                    );
            else if (gameObject.RectangleWithTexture.Rectangle.TopLeft.X > RectangleWithTexture.Rectangle.TopLeft.X)
                RectangleWithTexture.Rectangle = new
                    (
                        new Point
                        (
                            RectangleWithTexture.Rectangle.TopLeft.X + _unit.x,
                            RectangleWithTexture.Rectangle.TopLeft.Y,
                            RectangleWithTexture.Rectangle.TopLeft.Z
                        ),
                        RectangleWithTexture.Rectangle.GetWidth(),
                        RectangleWithTexture.Rectangle.GetHeight()
                    );

            if (Math.Abs(gameObject.RectangleWithTexture.Rectangle.TopLeft.Y - RectangleWithTexture.Rectangle.TopLeft.Y) < errrorRate)
            {
                // Для более точного фиксированного расположения игрока на позиции, иначе позиция обновляется в каждом кадре
            }
            else if (gameObject.RectangleWithTexture.Rectangle.TopLeft.Y < RectangleWithTexture.Rectangle.TopLeft.Y)
                RectangleWithTexture.Rectangle = new
                    (
                        new Point
                        (
                            RectangleWithTexture.Rectangle.TopLeft.X,
                            RectangleWithTexture.Rectangle.TopLeft.Y - _unit.y,
                            RectangleWithTexture.Rectangle.TopLeft.Z
                        ),
                        RectangleWithTexture.Rectangle.GetWidth(),
                        RectangleWithTexture.Rectangle.GetHeight()
                    );
            else if (gameObject.RectangleWithTexture.Rectangle.TopLeft.Y > RectangleWithTexture.Rectangle.TopLeft.Y)
                RectangleWithTexture.Rectangle = new
                    (
                        new Point
                        (
                            RectangleWithTexture.Rectangle.TopLeft.X,
                            RectangleWithTexture.Rectangle.TopLeft.Y + _unit.y,
                            RectangleWithTexture.Rectangle.TopLeft.Z
                        ),
                        RectangleWithTexture.Rectangle.GetWidth(),
                        RectangleWithTexture.Rectangle.GetHeight()
                    );

            ReturnTime(gameObject, errrorRate);
            this.SetPoints(RectangleWithTexture.Rectangle);
        }

        public bool ContinueMove()
        {
            return Time == FrameTime;
        }

        private void ReturnTime(GameObject gameObject, double errrorRate)
        {
            if (Math.Abs(gameObject.RectangleWithTexture.Rectangle.TopLeft.X - RectangleWithTexture.Rectangle.TopLeft.X) < errrorRate &&
                Math.Abs(gameObject.RectangleWithTexture.Rectangle.TopLeft.Y - RectangleWithTexture.Rectangle.TopLeft.Y) < errrorRate)
            {
                Time = FrameTime;
            }
        }
    }

}
