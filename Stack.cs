using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brouillon_projet
{
    public class Stack
    {
        private List<Card> cards;
        private Random random;

        public Stack(List<Card> initialCards)
        {
            cards = new List<Card>(initialCards);
            random = new Random();
        }

        public void Shuffle()
        {
            cards = cards.OrderBy(card => random.Next()).ToList();
        }

        public Card Draw()
        {
            if (cards.Count == 0)
            {
                Console.WriteLine("No card on the stack.");
                return null;
            }

            Card drawnCard = cards[cards.Count - 1];
            cards.RemoveAt(cards.Count - 1);
            return drawnCard;
        }

        public Card PeekTopCard()
        {
            if (cards.Count == 0)
            {
                Console.WriteLine("No card on the stack.");
                return null;
            }

            return cards[cards.Count - 1];
        }
    }
}
