using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3_Csharp_project
{
    public class Stack
    {
        private List<Card> cardslist;

        public Stack(List<Card> mycardslist)
        {
            this.cardslist = mycardslist;
        }

        public List<Card> Cardslist
        {
            get { return this.cardslist; }
            set { this.cardslist = value; }
        }

        public void StackInitialization()
        {
        }

        public void RandomMix()
        {
        }

        public Card PickCard()
        {
            Card card = new SpecialCard (3, "nombre", "R"); 
            return card; 
        }

        public void ResetStack()
        {
        }
    }
}
