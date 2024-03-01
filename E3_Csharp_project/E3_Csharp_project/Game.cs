using E3_Csharp_project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3_Csharp_project
{
    public class Game
    {
        private int id;
        private string state;
        private List<Player> playerslist;

        public Game(int myid, string mystate, List<Player> myplayers_list)
        {
            this.id = myid;
            this.state = mystate;
            this.playerslist = myplayers_list;
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public List<Player> PlayersList
        {
            get { return this.playerslist; }
            set { this.playerslist = value; }
        }

        public void AddPlayer()
        {
        }

        public void PlayerTurn()
        {
        }

        public void AddCard(Card card)
        {
        }

        public void StartGame()
        {
        }

        public void EndGame()
        {

        }
    }
}
