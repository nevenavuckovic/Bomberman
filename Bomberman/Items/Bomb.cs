using Bomberman.Game;
using Bomberman.Items.Enemies;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Bomberman.Items
{
    public class Bomb: Item
    {
        public int X { get; private set; }

        public int Y { get; private set; }
        private Thread thread;
        private List<Fire> fires;
        public Bomb(int x, int y)
        {
            X = x;
            Y = y;
            fires = new List<Fire>();
        }

        protected override void SetImage()
        {
            Image = new Image
            {
                Source = new BitmapImage(new Uri("../../Resources/Pictures/bomb.jpg", UriKind.Relative))
            };
        }

        public void Detonate(NewGame newGame, bool wait, bool remotely)
        {
            thread = new Thread(() => Explode(newGame, wait, remotely));
            thread.Start();
        }

        private void Explode(NewGame newGame, bool wait, bool remotely)
        {
            if (wait)
            {
                Thread.Sleep(1000);
            }

            if (!remotely)
            {
                newGame.ActiveBombs.Remove(this);
                newGame.Player.BombsLeft++;
            }
            newGame.GameWindow.Dispatcher.Invoke(() =>
            {
                bool stopLeft = false;
                bool stopRight = false;
                bool stopUp = false;
                bool stopDown = false;

                newGame.GameWindow.LevelMap.Children.Remove(Image);
                Fire fire = new Fire(X, Y);
                Grid.SetRow(fire.Image, X);
                Grid.SetColumn(fire.Image, Y);
                newGame.Items[X, Y] = fire;
                fires.Add(fire);
                newGame.GameWindow.LevelMap.Children.Add(fire.Image);
                if (newGame.Player.X == X && newGame.Player.Y == Y)
                {
                    newGame.GameFinished(true);
                }

                for (int k = 1; k <= newGame.Player.FireRange; k++)
                {
                    FireUp(newGame, ref stopUp, X, Y - k, remotely);
                    FireUp(newGame, ref stopLeft, X - k, Y, remotely);
                    FireUp(newGame, ref stopRight, X + k, Y, remotely);
                    FireUp(newGame, ref stopDown, X, Y + k, remotely);
                }
                
            });
            Thread.Sleep(500);
            newGame.GameWindow.Dispatcher.Invoke(() =>
            {
                foreach (Fire fire in fires)
                {
                    newGame.Items[fire.X, fire.Y] = null;
                    newGame.GameWindow.LevelMap.Children.Remove(fire.Image);
                }
                fires.Clear();
            });


        }


        private void FireUp(NewGame newGame, ref bool stop, int i, int j, bool remotely)
        {
            if (!stop)
            {
                if (newGame.Player.X == i && newGame.Player.Y == j)
                {
                    newGame.GameFinished(true);
                   
                }
                else if (newGame.Items[i, j] == null && newGame.Enemies[i, j] == null)
                {
                    Fire fire = new Fire(i, j);
                    fires.Add(fire);
                    Grid.SetRow(fire.Image, i);
                    Grid.SetColumn(fire.Image, j);
                    newGame.Items[i, j] = fire;
                    newGame.GameWindow.LevelMap.Children.Add(fire.Image);
                }
                else if (newGame.Enemies[i, j] != null)
                {
                    Enemy enemy = newGame.Enemies[i, j];
                    enemy.DestroyMe(newGame, true);
                    newGame.GameWindow.ScoreLabel.Content = "Score: " + newGame.Player.Score;
                    if (newGame.Items[i, j] != null && Equals(newGame.Items[i, j].GetType(), typeof(Wall)))
                    {
                        newGame.GameWindow.LevelMap.Children.Remove(newGame.Items[i, j].Image);
                        newGame.Items[i, j] = null;
                        stop = true;
                        newGame.Player.Score += 10;
                        newGame.GameWindow.ScoreLabel.Content = "Score: " + newGame.Player.Score;
                    }
                    Fire fire = new Fire(i, j);
                    fires.Add(fire);
                    Grid.SetRow(fire.Image, i);
                    Grid.SetColumn(fire.Image, j);
                    newGame.Items[i, j] = fire;
                    newGame.GameWindow.LevelMap.Children.Add(fire.Image);
                }
                else if (Equals(newGame.Items[i, j].GetType(), typeof(Wall)))
                {
                    newGame.GameWindow.LevelMap.Children.Remove(newGame.Items[i, j].Image);
                    newGame.Items[i, j] = null;
                    stop = true;
                    newGame.Player.Score += 10;
                    newGame.GameWindow.ScoreLabel.Content = "Score: " + newGame.Player.Score;
                }
                else if (Equals(newGame.Items[i, j].GetType(), typeof(Barrier)))
                {
                    stop = true;
                }
                else if (Equals(newGame.Items[i, j].GetType(), typeof(Bomb)))
                {
                    Bomb bomb = (Bomb)newGame.Items[i, j];
                    bomb.DestroyMe(newGame);
                    bomb.Detonate(newGame, false, remotely);
                }

            }
        }

        public void DestroyMe(NewGame newGame)
        {
            if (thread != null)
            {
                thread.Abort();
                foreach (Fire fire in fires)
                {
                    newGame.Items[fire.X, fire.Y] = null;
                    newGame.GameWindow.LevelMap.Children.Remove(fire.Image);
                }
                fires.Clear();
            }
        }
        
    }
}
