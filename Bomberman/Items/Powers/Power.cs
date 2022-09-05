namespace Bomberman.Items.Powers
{
    public abstract class Power: Item
    {
        public int X { get; protected set; }

        public int Y { get; protected set; }

        public Power(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
