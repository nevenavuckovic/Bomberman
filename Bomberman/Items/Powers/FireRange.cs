using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items.Powers
{
    public class FireRange: Power
    {
        public FireRange(int x, int y): base(x, y)
        {
        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/powerfirerange.png", UriKind.Relative))
            };
        }
    }
}
