using Bomberman.Game;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items.Enemies
{
    public class Pontan: Enemy
    {
        public Pontan(int x, int y) : base(x, y)
        {

        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/pontan.gif", UriKind.Relative))
            };
        }

        protected override void SetPoints()
        {
            Points = 1500;
        }

        protected override void Move(NewGame newGame)
        {
            while (Alive)
            {
                Thread.Sleep(400);
                newGame.GameWindow.Dispatcher.Invoke(() =>
                {
                    newGame.Enemies[X, Y] = null;
                    newGame.GameWindow.LevelMap.Children.Remove(Image);
                    Item[,] items = newGame.Items;
                    Enemy[,] enemies = newGame.Enemies;
                    Player player = newGame.Player;
                    List<KeyValuePair<int, int>> availablePositions = new List<KeyValuePair<int, int>>();

                    if (enemies[X + 1, Y] == null
                        && (items[X + 1, Y] == null
                            || Equals(items[X + 1, Y].GetType(), typeof(Fire))
                            || Equals(items[X + 1, Y].GetType(), typeof(Wall))
                            && !Equals(items[X + 1, Y].GetType(), typeof(Bomb))))
                    {
                        availablePositions.Add(new KeyValuePair<int, int>(X + 1, Y));
                    }
                    if (enemies[X - 1, Y] == null
                        && (items[X - 1, Y] == null
                            || Equals(items[X - 1, Y].GetType(), typeof(Fire))
                            || Equals(items[X - 1, Y].GetType(), typeof(Wall))
                            && !Equals(items[X - 1, Y].GetType(), typeof(Bomb))))
                    {
                        availablePositions.Add(new KeyValuePair<int, int>(X - 1, Y));
                    }
                    if (enemies[X, Y + 1] == null
                        && (items[X, Y + 1] == null
                            || Equals(items[X, Y + 1].GetType(), typeof(Fire))
                            || Equals(items[X, Y + 1].GetType(), typeof(Wall))
                            && !Equals(items[X, Y + 1].GetType(), typeof(Bomb))))
                    {
                        availablePositions.Add(new KeyValuePair<int, int>(X, Y + 1));
                    }
                    if (enemies[X, Y - 1] == null
                        && (items[X, Y - 1] == null
                            || Equals(items[X, Y - 1].GetType(), typeof(Fire))
                            || Equals(items[X, Y - 1].GetType(), typeof(Wall))
                            && !Equals(items[X, Y - 1].GetType(), typeof(Bomb))))
                    {
                        availablePositions.Add(new KeyValuePair<int, int>(X, Y - 1));
                    }

                    int size = availablePositions.Count;
                    if (size > 0)
                    {
                        Random random = new Random();
                        int rand = random.Next(100);

                        X = availablePositions[rand % size].Key;
                        Y = availablePositions[rand % size].Value;
                    }

                    newGame.Enemies[X, Y] = this;

                    Grid.SetRow(Image, X);
                    Grid.SetColumn(Image, Y);
                    newGame.GameWindow.LevelMap.Children.Add(Image);
                    if (player.X == X && player.Y == Y)
                    {
                        newGame.GameFinished(true);
                    }
                    if (items[X, Y] != null && items[X, Y].GetType().Equals(typeof(Fire)))
                    {
                        DestroyMe(newGame, true);
                    }
                });
            }
        }
    }
}
