using Bomberman.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bomberman
{
    /// <summary>
    /// Interaction logic for AddNewPlayerWindow.xaml
    /// </summary>
    public partial class AddNewPlayerWindow : Window
    {
        public AddNewPlayerWindow()
        {
            InitializeComponent();
            NewPlayerName.Focus();      
        }

        private void NewPlayerNameOKButton_Click(object sender, RoutedEventArgs e)
        {
            if (NewPlayerName.Text.Length > 0)
            {
                if (NewPlayerName.Text.Length < 20)
                {
                    bool same = false;
                    foreach(Player player in MainWindow.players) { 
                        if (player.PlayersName.Equals(NewPlayerName.Text))
                        {
                            same = true;
                            break;
                        }
                    }
                    bool onlyLetters = true;
                    foreach (char c in NewPlayerName.Text)
                    {
                        if (!char.IsLetter(c))
                        {
                            onlyLetters = false;
                            break;
                        }
                    }

                    if (same)
                    {
                        MessageBox.Show("Player with that name already exist. Please choose another one!", "Name already exist", MessageBoxButton.OK);
                    } else if (!onlyLetters) {
                        MessageBox.Show("Player's name must contain only letters. Please choose another one!", "Not only letters", MessageBoxButton.OK);
                    }
                    else
                    {
                        DialogResult = true;
                    }
                }
                else
                {
                    MessageBox.Show("Player's name length must be less than 20. Please choose another one!", "Name too long", MessageBoxButton.OK);
                }
            } else
            {
                MessageBox.Show("Player's name can't be empty string. Please choose another one!", "Name empty string", MessageBoxButton.OK);
            }
        }

        public string Answer()
        {
            return NewPlayerName.Text;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (NewPlayerName.Text.Length > 0)
                {
                    if (NewPlayerName.Text.Length < 20)
                    {
                        bool same = false;
                        foreach (Player player in MainWindow.players)
                        {
                            if (player.PlayersName.Equals(NewPlayerName.Text))
                            {
                                same = true;
                                break;
                            }
                        }
                        bool onlyLetters = true;
                        foreach (char c in NewPlayerName.Text)
                        {
                            if (!char.IsLetter(c))
                            {
                                onlyLetters = false;
                                break;
                            }
                        }

                        if (same)
                        {
                            MessageBox.Show("Player with that name already exist. Please choose another one!", "Name already exist", MessageBoxButton.OK);
                        }
                        else if (!onlyLetters)
                        {
                            MessageBox.Show("Player's name must contain only letters. Please choose another one!", "Not only letters", MessageBoxButton.OK);
                        }
                        else
                        {
                            DialogResult = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Player's name length must be less than 20. Please choose another one!", "Name too long", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Player's name can't be empty string. Please choose another one!", "Name empty string", MessageBoxButton.OK);
                }
            }
        }
    }
}
