using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3_Csharp_project
{
    public class Score
    {
        private Dictionary<int, int> scoresdict;

        public Score(Dictionary<int, int> myscoresdict)
        {
            this.scoresdict = myscoresdict;
        }

        public Dictionary<int, int> Scoresdict
        {
            get { return this.scoresdict; }
            set { this.scoresdict = value; }
        }

        public void AddScore(int idplayer, int score)
        {
        }

        public void PrintScore()
        {
        }

        public void ResetScore()
        {
        }
    }
}
