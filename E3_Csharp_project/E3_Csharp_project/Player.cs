using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3_Csharp_project
{
    public class Player
    {
        private int id;
        private string name; 
        private List<Card> playercards;
        private Score score; 

        public Player(int myid, string myname, List<Card> myplayercards, Score myscore)
        {
            this.id = myid;
            this.name = myname;
            this.playercards = myplayercards;
            this.score = myscore; 
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public List<Card> Playercards
        {
            get { return this.playercards; }
            set { this.playercards = value; }
        }

        public Score Score
        {
            get { return this.score; }
            set { this.score = value; }
        }
        public void PickCard(Stack stack)
        {
        }

        public void PlayCard(Card card, Game game)
        {
        }

        public void PrintAvailableCards()
        {
        }
    }
}
