using System.Windows.Controls;

namespace Bomberman.Items
{
    public abstract class Item
    {
        public Image Image { get; protected set; }

        public Item()
        {
            SetImage();
        }

        protected abstract void SetImage();
    }
}
