using Bomberman.Game;
using Bomberman.Items;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Bomberman
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        private readonly NewGame newGame;

        public GameWindow(Player player)
        {
            InitializeComponent();

            Rows = 13;
            Columns = 31;

            int imageWidth = 40;
            int imageHeight = 40;

            for (int i = 0; i < Rows; i++)
            {
                RowDefinition row = new RowDefinition
                {
                    Height = new GridLength(imageHeight)
                };
                LevelMap.RowDefinitions.Add(row);
            }
            for (int j = 0; j < Columns; j++)
            {
                ColumnDefinition column = new ColumnDefinition
                {
                    Width = new GridLength(imageWidth)
                };
                LevelMap.ColumnDefinitions.Add(column);
            }

            newGame = new NewGame(this, player);

        }


        private void LevelMap_Loaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(LevelMap);
        }

        private void LevelMap_KeyDown(object sender, KeyEventArgs e)
        {
            int i = newGame.Player.X, j = newGame.Player.Y;

            if (e.Key == Key.Left)
            {
                newGame.MoveBomberman(i, j - 1);
            }
            if (e.Key == Key.Up)
            {
                newGame.MoveBomberman(i - 1, j); 
            }
            if (e.Key == Key.Right)
            {
                newGame.MoveBomberman(i, j + 1);
            }
            if (e.Key == Key.Down)
            {
                newGame.MoveBomberman(i + 1, j);  
            }
            if (e.Key == Key.B)
            {
                newGame.PlaceBomb(i, j);
            }
            if (e.Key == Key.Space)
            {
                newGame.BombsExplode();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            newGame.ClosingGame();
        }

        



    }
}
