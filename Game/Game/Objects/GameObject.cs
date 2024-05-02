namespace Game
{
    public abstract class GameObject : Obj, IObjectCore, IGameCore
    {
        public (int x, int y) Index { get; set; }
        public bool IsSolid { get; set; }
        protected GameObject(RectangleWithTexture rectangleWithTexture, Texture texture, (int x, int y) index) :
            base(rectangleWithTexture, texture)
        {
            Index = index;
            IsSolid = false;
        }

        protected GameObject()
        {
            IsSolid = false;
        }
    }
}