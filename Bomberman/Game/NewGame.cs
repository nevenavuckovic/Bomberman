using Bomberman.Items;
using Bomberman.Items.Enemies;
using Bomberman.Items.Powers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bomberman.Game
{
    public class NewGame
    {
        private int level;
        private int time;
        private Thread timeThread;
        private Power power;
        private Door door;
        private static bool gameFinished = false;
        public Player Player { get; private set; }
        public Item[,] Items { get; private set; }
        public Enemy[,] Enemies { get; private set; }
        public List<Bomb> ActiveBombs { get; private set; }
        public GameWindow GameWindow { get; private set; }

        public NewGame(GameWindow gameWindow, Player player)
        {
            GameWindow = gameWindow;
            Player = player;
            level = 1;
            gameWindow.PlayersNameLabel.Content = "Player: " + Player.PlayersName;
            gameWindow.ScoreLabel.Content = "Score: " + Player.Score;
            gameFinished = false;
            LoadLevel(level);
        }

        private void LoadLevel(int level)
        {
            if (timeThread != null)
            {
                timeThread.Abort();
            }
            if (level < 6)
            {
                if (ActiveBombs != null)
                {
                    foreach (Bomb bomb in ActiveBombs)
                    {
                        bomb.DestroyMe(this);
                    }
                }
                GameWindow.LevelMap.Children.Clear();
                power = null;
                door = null;
                GameWindow.LevelLabel.Content = "Level: " + level;
                time = 300 + (level - 1) * 50;
                GameWindow.TimeLabel.Content = "Time: " + time;
                Items = new Item[GameWindow.Rows, GameWindow.Columns];
                ActiveBombs = new List<Bomb>();
                Enemies = new Enemy[GameWindow.Rows, GameWindow.Columns];

                StreamReader sr = new StreamReader("../../Resources/Levels/level" + level + ".txt");
                string[,] map = new string[GameWindow.Rows, GameWindow.Columns];
                string[] line = new string[GameWindow.Columns];
                for (int i = 0; i < GameWindow.Rows; i++)
                {
                    line = sr.ReadLine().Split('.');
                    for (int j = 0; j < GameWindow.Columns; j++)
                    {
                        map[i, j] = line[j];
                    }
                }

                LoadPowers(map, level);

                Image image = null;
                for (int i = 0; i < GameWindow.Rows; i++)
                {
                    for (int j = 0; j < GameWindow.Columns; j++)
                    {
                        if (!map[i, j].Equals(" "))
                        {
                            if (map[i, j].Equals("b"))
                            {
                                Items.Barrier barrier = new Items.Barrier();
                                Items[i, j] = barrier;
                                image = barrier.Image;

                            }
                            else if (map[i, j].Equals("w"))
                            {
                                Wall wall = new Wall();
                                Items[i, j] = wall;
                                image = wall.Image;
                            }
                            else if (map[i, j].Equals("1"))
                            {
                                Enemy ballom = new Ballom(i, j);
                                Enemies[i, j] = ballom;
                                image = ballom.Image;
                                ballom.StartMoving(this);
                            }
                            else if (map[i, j].Equals("2"))
                            {
                                Enemy onil = new Onil(i, j);
                                Enemies[i, j] = onil;
                                image = onil.Image;
                                onil.StartMoving(this);
                            }
                            else if (map[i, j].Equals("3"))
                            {
                                Enemy ovape = new Ovape(i, j);
                                Enemies[i, j] = ovape;
                                image = ovape.Image;
                                ovape.StartMoving(this);
                            }
                            else if (map[i, j].Equals("4"))
                            {
                                Enemy pontan = new Pontan(i, j);
                                Enemies[i, j] = pontan;
                                image = pontan.Image;
                                pontan.StartMoving(this);
                            }
                            Grid.SetRow(image, i);
                            Grid.SetColumn(image, j);
                            GameWindow.LevelMap.Children.Add(image);
                        }
                    }
                }

                Player.X = 1;
                Player.Y = 1;
                Player.BombsLeft = Player.BombsCount;
                Grid.SetRow(Player.Image, Player.X);
                Grid.SetColumn(Player.Image, Player.Y);
                GameWindow.LevelMap.Children.Add(Player.Image);

                timeThread = new Thread(StartTime);
                timeThread.Start();
            }
            else
            {
                GameFinished(false);
            }
        }


        private void LoadPowers(string[,] map, int level)
        {
            Random random = new Random();
            int i = random.Next(GameWindow.Rows);
            int j = random.Next(GameWindow.Columns);
            while (!map[i, j].Equals("w"))
            {
                i = random.Next(GameWindow.Rows);
                j = random.Next(GameWindow.Columns);
            }
            if (level == 1)
            {
                FireRange fireRange = new FireRange(i, j);
                power = fireRange;
                Grid.SetRow(fireRange.Image, i);
                Grid.SetColumn(fireRange.Image, j);
                GameWindow.LevelMap.Children.Add(fireRange.Image);

                i = random.Next(GameWindow.Rows);
                j = random.Next(GameWindow.Columns);
                while (!map[i, j].Equals("w") || (i == fireRange.X && j == fireRange.Y))
                {
                    i = random.Next(GameWindow.Rows);
                    j = random.Next(GameWindow.Columns);
                }
                door = new Door(i, j);
                Grid.SetRow(door.Image, i);
                Grid.SetColumn(door.Image, j);
                GameWindow.LevelMap.Children.Add(door.Image);

            }
            else if (level == 2)
            {
                BombUp bombUp = new BombUp(i, j);
                power = bombUp;
                Grid.SetRow(bombUp.Image, i);
                Grid.SetColumn(bombUp.Image, j);
                GameWindow.LevelMap.Children.Add(bombUp.Image);

                i = random.Next(GameWindow.Rows);
                j = random.Next(GameWindow.Columns);
                while (!map[i, j].Equals("w") || (i == bombUp.X && j == bombUp.Y))
                {
                    i = random.Next(GameWindow.Rows);
                    j = random.Next(GameWindow.Columns);
                }
                door = new Door(i, j);
                Grid.SetRow(door.Image, i);
                Grid.SetColumn(door.Image, j);
                GameWindow.LevelMap.Children.Add(door.Image);
            }
            else if (level == 3)
            {
                BombRemoteControl bombRemoteControl = new BombRemoteControl(i, j);
                power = bombRemoteControl;
                Grid.SetRow(bombRemoteControl.Image, i);
                Grid.SetColumn(bombRemoteControl.Image, j);
                GameWindow.LevelMap.Children.Add(bombRemoteControl.Image);

                i = random.Next(GameWindow.Rows);
                j = random.Next(GameWindow.Columns);
                while (!map[i, j].Equals("w") || (i == bombRemoteControl.X && j == bombRemoteControl.Y))
                {
                    i = random.Next(GameWindow.Rows);
                    j = random.Next(GameWindow.Columns);
                }
                door = new Door(i, j);
                Grid.SetRow(door.Image, i);
                Grid.SetColumn(door.Image, j);
                GameWindow.LevelMap.Children.Add(door.Image);


            }
            else if (level == 4)
            {
                BombUp bombUp = new BombUp(i, j);
                power = bombUp;
                Grid.SetRow(bombUp.Image, i);
                Grid.SetColumn(bombUp.Image, j);
                GameWindow.LevelMap.Children.Add(bombUp.Image);

                i = random.Next(GameWindow.Rows);
                j = random.Next(GameWindow.Columns);
                while (!map[i, j].Equals("w") || (i == bombUp.X && j == bombUp.Y))
                {
                    i = random.Next(GameWindow.Rows);
                    j = random.Next(GameWindow.Columns);
                }
                door = new Door(i, j);
                Grid.SetRow(door.Image, i);
                Grid.SetColumn(door.Image, j);
                GameWindow.LevelMap.Children.Add(door.Image);

            }
            else
            {
                FireRange fireRange = new FireRange(i, j);
                power = fireRange;
                Grid.SetRow(fireRange.Image, i);
                Grid.SetColumn(fireRange.Image, j);
                GameWindow.LevelMap.Children.Add(fireRange.Image);

                i = random.Next(GameWindow.Rows);
                j = random.Next(GameWindow.Columns);
                while (!map[i, j].Equals("w") || (i == fireRange.X && j == fireRange.Y))
                {
                    i = random.Next(GameWindow.Rows);
                    j = random.Next(GameWindow.Columns);
                }
                door = new Door(i, j);
                Grid.SetRow(door.Image, i);
                Grid.SetColumn(door.Image, j);
                GameWindow.LevelMap.Children.Add(door.Image);
            }
        }

        private void StartTime()
        {
            while (time > 0)
            {
                Thread.Sleep(1000);
                time--;
                GameWindow.Dispatcher.Invoke(() =>
                {
                    GameWindow.TimeLabel.Content = "Time: " + time;
                });
            }
            GameWindow.Dispatcher.Invoke(() =>
            {
                GameFinished(true);
            });
        }

        public void MoveBomberman(int i, int j)
        {
            if (Items[i, j] == null)
            {
                GameWindow.LevelMap.Children.Remove(Player.Image);
                Player.X = i;
                Player.Y = j;
                Grid.SetRow(Player.Image, i);
                Grid.SetColumn(Player.Image, j);
                GameWindow.LevelMap.Children.Add(Player.Image);

                if (i == door.X && j == door.Y)
                {
                    bool allKilled = true;
                    for (int k = 0; k < GameWindow.Rows; k++)
                    {
                        for (int l = 0; l < GameWindow.Columns; l++)
                        {
                            if (Enemies[k, l] != null)
                            {
                                allKilled = false;
                                break;
                            }
                        }
                    }
                    if (allKilled)
                    {
                        level++;
                        Player.Score += time * 10;
                        GameWindow.ScoreLabel.Content = "Score: " + Player.Score;
                        LoadLevel(level);
                    }

                }
                else if (power != null && i == power.X && j == power.Y)
                {
                    if (Equals(power.GetType(), typeof(BombRemoteControl)))
                    {
                        Player.RemoteControl = true;

                    }
                    else if (Equals(power.GetType(), typeof(BombUp)))
                    {
                        Player.BombsCount++;
                        Player.BombsLeft++;
                    }
                    else if (Equals(power.GetType(), typeof(FireRange)))
                    {
                        Player.FireRange++;
                    }
                    Player.Score += 500;
                    GameWindow.ScoreLabel.Content = "Score: " + Player.Score;
                    GameWindow.LevelMap.Children.Remove(power.Image);
                    power = null;
                }
                else if (Enemies[i, j] != null)
                {
                    GameFinished(true);
                }
            }
            else if (Equals(Items[i, j].GetType(), typeof(Fire)))
            {
                GameFinished(true);
            }
        }

        public void PlaceBomb(int i, int j)
        {
            if (Player.HasBomb())
            {
                if (Items[i, j] == null)
                {
                    Bomb bomb = new Bomb(i, j);
                    ActiveBombs.Add(bomb);
                    Items[i, j] = bomb;
                    Player.BombsLeft--;
                    Grid.SetRow(bomb.Image, i);
                    Grid.SetColumn(bomb.Image, j);
                    GameWindow.LevelMap.Children.Add(bomb.Image);

                    if (!Player.RemoteControl)
                    {
                        bomb.Detonate(this, true, false);
                    }

                }
            }
        }
        public void BombsExplode()
        {
            if (Player.RemoteControl && ActiveBombs.Count > 0)
            {
                foreach (Bomb bomb in ActiveBombs)
                {
                    bomb.Detonate(this, false, true);
                }
                Player.BombsLeft += ActiveBombs.Count;
                ActiveBombs.Clear();
            }
        }
        public void GameFinished(bool gameOver)
        {
            if (!gameFinished)
            {
                GameFinishedWindow gameOverWindow = new GameFinishedWindow(gameOver, Player);
                gameOverWindow.Show();
                GameWindow.Close();
            }
            gameFinished = true;

        }
        public void ClosingGame()
        {
            timeThread.Abort();
            for (int i = 0; i < GameWindow.Rows; i++)
            {
                for (int j = 0; j < GameWindow.Columns; j++)
                {
                    if (Enemies[i, j] != null)
                    {
                        Enemies[i, j].DestroyMe(this, false);
                    }
                }
            }
            if (ActiveBombs != null)
            {
                foreach (Bomb bomb in ActiveBombs)
                {
                    bomb.DestroyMe(this);
                }
            }
        }




    }
}
