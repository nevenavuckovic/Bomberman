using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items
{
    public class Door: Item
    {
        public int X { get; private set; }

        public int Y { get; private set; }
        public Door(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/door.png", UriKind.Relative))
            };
        }
    }
}
