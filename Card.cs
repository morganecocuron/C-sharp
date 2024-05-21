using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brouillon_projet
{
    public class Card
    {
        public int Id { get; }
        public CardType Type { get; }
        public CardColor Color { get; }
        public int? Number { get; } // Utilisation d'un type nullable pour le nombre

        public Card(int id, CardType type, CardColor color, int? number = null)
        {
            Id = id;
            Type = type;
            Color = color;
            Number = number;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"ID: {Id}, Number: {(Number.HasValue ? Number.ToString() : "null")}, Type: {Type}, Color: {Color}");
        }
    }
}
