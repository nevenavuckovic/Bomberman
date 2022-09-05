using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items
{
    public class Fire : Item
    {
        public int X { get; private set; }

        public int Y { get; private set; }
        public Fire(int x, int y)
        {
            X = x;
            Y = y;
        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/fire.png", UriKind.Relative))
            };
        }
    }
}
