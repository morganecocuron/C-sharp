using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brouillon_projet
{
    public class Score
    {
        public int Points { get; private set; }

        public Score()
        {
            Points = 0;
        }

        public void AddPoints(int points)
        {
            Points += points;
        }
    }
}
