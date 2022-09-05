using Bomberman.Game;
using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items.Enemies
{
    public class Ballom : Enemy
    {
        public Ballom(int x, int y) : base(x, y)
        {

        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/ballom.png", UriKind.Relative))
            };
        }

        protected override void SetPoints()
        {
            Points = 100;
        }

        protected override void Move(NewGame newGame)
        {
            Random random = new Random();
            int rand = random.Next(20);

            int direction = rand % 2 == 0 ? 1 : -1;

            while (Alive)
            {
                Thread.Sleep(1000);
                newGame.GameWindow.Dispatcher.Invoke(() =>
                {
                    newGame.Enemies[X, Y] = null;
                    newGame.GameWindow.LevelMap.Children.Remove(Image);
                    Item item = newGame.Items[X, Y + direction];
                    Enemy enemy = newGame.Enemies[X, Y + direction];

                    bool gameOver = false;
                    bool killMe = false;
                    if (item != null && Equals(item.GetType(), typeof(Fire)))
                    {
                        killMe = true;
                        Y += direction;
                    }
                    else if (item != null || enemy != null)
                    {
                        direction = -direction;
                    }
                    else if (newGame.Player.X == X && newGame.Player.Y == Y + direction)
                    {
                        gameOver = true;
                        Y += direction;
                    }
                    else if (item == null && enemy == null)
                    {
                        Y += direction;
                    }


                    newGame.Enemies[X, Y] = this;

                    Grid.SetRow(Image, X);
                    Grid.SetColumn(Image, Y);
                    newGame.GameWindow.LevelMap.Children.Add(Image);
                    if (gameOver) {
                        newGame.GameFinished(true);
                    }
                    if (killMe) {
                        DestroyMe(newGame, true);
                    }
                });
            }
        }
    }
}
