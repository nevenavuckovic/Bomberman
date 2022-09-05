using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items
{
    public class Player : Item
    {
        public string PlayersName { get; private set; }

        public int X { get; set; }

        public int Y { get; set; }
        public int BombsCount { get; set; }
        public int BombsLeft { get; set; }
        public int FireRange { get; set; }

        public int Score { get; set; }
        public bool RemoteControl { get; set; }

        public Player(string name)
        {
            PlayersName = name;
            X = 1;
            Y = 1;
            BombsCount = 1;
            BombsLeft = 1;
            FireRange = 1;
            Score = 0;
            RemoteControl = false;
        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/bomberman2.png", UriKind.Relative))
            };
        }

        public bool HasBomb()
        {
            return BombsLeft > 0;
        }

    }
}
