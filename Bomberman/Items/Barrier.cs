using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items
{
    public class Barrier: Item
    {
        public Barrier()
        {
        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/barrier.png", UriKind.Relative))
            };
        }
    }
}
