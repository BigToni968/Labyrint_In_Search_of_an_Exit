namespace Game
{
    public interface IItemUpCallback
    {
        public delegate void ItemUpDelegate(int totalKeys);
        public event ItemUpDelegate ItemUpCallback;
    }

    public interface IItemUp : IItemUpCallback
    {
        public void Take();
    }
}
