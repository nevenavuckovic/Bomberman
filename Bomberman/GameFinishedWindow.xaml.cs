using Bomberman.Game;
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
using System.Windows.Shapes;

namespace Bomberman
{
    /// <summary>
    /// Interaction logic for GameFinishedWindow.xaml
    /// </summary>
    public partial class GameFinishedWindow : Window
    {
        public GameFinishedWindow(bool gameOver, Player player)
        {
            InitializeComponent();

            if (gameOver)
            {
                GameFinishedImage.Source = new BitmapImage(new Uri("../../Resources/Pictures/gameover.jpg", UriKind.Relative));
            }
            else
            {
                GameFinishedImage.Source = new BitmapImage(new Uri("../../Resources/Pictures/congratulations.jpg", UriKind.Relative));
            }

            PlayersScoreTextBlock.Text = "Your score = " + player.Score;

            List<HighScore> highScores = new List<HighScore>();

            StreamReader sr = new StreamReader("../../Resources/Players/highscores.txt");

            string line;
            string[] splited;
            while ((line = sr.ReadLine()) != null)
            {
                splited = line.Split('.');
                splited = splited[1].Split('=');
                highScores.Add(new HighScore(splited[0].Trim(), Int32.Parse(splited[1])));
            }
            sr.Close();
            if (player.Score > 0)
            {
                highScores.Add(new HighScore(player.PlayersName, player.Score));

                highScores.Sort();
            }

            string resault = "";

            StreamWriter sw = new StreamWriter("../../Resources/Players/highscores.txt", false);
            for(int i = 0; i < highScores.Count && i < 5; i++)
            {
                line = (i + 1).ToString() + ". " + highScores[i].PlayerName + " = " + highScores[i].Score.ToString();
                sw.WriteLine(line);
                resault += line + "\n";
            }
            sw.Close();

            HighScoreTextBlock.Text = resault;
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }


}
