using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items
{
    public class Wall: Item
    {

        public Wall()
        {
        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/wall.png", UriKind.Relative))
            };
        }
    }
}
