using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brouillon_projet
{
    public class Player
    {
        public List<Card> Hand { get; }

        public Player()
        {
            Hand = new List<Card>();
        }

        public void DrawCard(Card card)
        {
            Hand.Add(card);
        }

        public void DisplayHand()
        {
            Console.WriteLine("Your hand:");
            foreach (Card card in Hand)
            {
                card.DisplayDetails();
            }
        }

        public bool HasPlayableCard(Card topCard)
        {
            foreach (Card card in Hand)
            {
                if (card.Color == topCard.Color || card.Type == topCard.Type || (card.Number.HasValue && card.Number == topCard.Number))
                {
                    return true;
                }
            }
            return false;
        }

        public bool RemoveCard(Card card)
        {
            return Hand.Remove(card);
        }

        public bool IsEmpty()
        {
            return Hand.Count == 0;
        }
    }

}
