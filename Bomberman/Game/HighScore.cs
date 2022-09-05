using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman.Game
{
    public class HighScore : IComparable
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }

        public HighScore(string name, int score)
        {
            PlayerName = name;
            Score = score;
        }

        public int CompareTo(object obj)
        {
            HighScore otherScore = (HighScore)obj;
            if (Score == otherScore.Score)
                return 0;

            if (Score < otherScore.Score)
                return 1;

            return -1;
        }
    }
}
