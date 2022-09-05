using Bomberman.Items;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bomberman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static List<Player> players;
        public MainWindow()
        {
            InitializeComponent();
            players = new List<Player>();
            StreamReader sr = new StreamReader("../../Resources/Players/players.txt");
            string player;
            while ((player = sr.ReadLine()) != null)
            {
                players.Add(new Player(player));
            }
            sr.Close();
            PlayersComboBox.ItemsSource = players;
            if (players.Count > 0)
            {
                PlayersComboBox.SelectedIndex = 0;
            }
            

        }

        private void AddNewPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewPlayerWindow addNewPlayer = new AddNewPlayerWindow();
            if (addNewPlayer.ShowDialog() == true)
            {
                players.Add(new Player(addNewPlayer.Answer()));

                StreamWriter sw = new StreamWriter("../../Resources/Players/players.txt", append: true);
                sw.WriteLine(addNewPlayer.Answer());
                sw.Close();

                PlayersComboBox.ItemsSource = null;
                PlayersComboBox.ItemsSource = players;
                PlayersComboBox.SelectedIndex = players.Count - 1;
            }

        }

        private void HighScoresButton_Click(object sender, RoutedEventArgs e)
        {
            HighScoreWindow highScoreWindow = new HighScoreWindow();
            highScoreWindow.ShowDialog();
        }

        private void StartNewGameButton_Click(object sender, RoutedEventArgs e)
        {
            if(PlayersComboBox.SelectedItem != null)
            {
                GameWindow gw = new GameWindow((Player)PlayersComboBox.SelectedItem);
                gw.Show();
                Close();
            } else
            {
                MessageBox.Show("You must select player's name to start the game. Please choose one or add a new one.", "Select name", MessageBoxButton.OK);
            }
            

        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow addNewPlayer = new HelpWindow();
            addNewPlayer.ShowDialog();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
