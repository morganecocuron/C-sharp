using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3_Csharp_project
{
    public class SpecialCard : Card
    {
        public SpecialCard(int myid, string mytype, string mycolor) : base(myid, mytype, mycolor)
        {
          
        }

        public void DisplayDetails()
        {
            base.DisplayDetails(); 
        }

        public void ActivateEffect()
        {

        }
    }
}
