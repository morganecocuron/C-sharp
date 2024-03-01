using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E3_Csharp_project
{
    public abstract class Card
    {
        private int id;
        private string type;
        private string color; 

        public Card(int myid, string mytype, string mycolor)
        {
            this.id = myid;
            this.type = mytype;
            this.color = mycolor; 
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public string Color
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public bool IsCompatible (Card previouscard)
        {
            bool iscompatible = false;
            return iscompatible; 
        }

        public void DisplayDetails()
        {
        }
    }
}
