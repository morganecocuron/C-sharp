using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brouillon_projet
{
    public class NumberCard : Card
    {
        public NumberCard(int id, CardColor color, int number) : base(id, CardType.Number, color, number)
        {
        }
    }
}
