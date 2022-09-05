using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items.Powers
{
    public class BombUp: Power
    {
        public BombUp(int x, int y): base(x, y)
        {

        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/powerbombup.png", UriKind.Relative))
            };
        }
    }
}
