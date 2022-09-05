using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items.Powers
{
    public class BombRemoteControl: Power
    { 
        public BombRemoteControl(int x, int y) : base(x, y)
        {

        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/powerbombremotecontrol.png", UriKind.Relative))
            };
        }
    }
}
