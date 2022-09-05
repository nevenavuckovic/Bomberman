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
    /// Interaction logic for HighScoreWindow.xaml
    /// </summary>
    public partial class HighScoreWindow : Window
    {
        public HighScoreWindow()
        {
            InitializeComponent();

            StreamReader sr = new StreamReader("../../Resources/Players/highscores.txt");
            string highScores = "";
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                highScores += line + "\n";
            }
            sr.Close();

            HighScoreTextBlock.Text = highScores;
        }
    }
}
