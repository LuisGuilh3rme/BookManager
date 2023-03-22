using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.CreateBook
{
    internal class ISBN
    {
        public int GTIN { get; private set; }
        public int Group { get; private set; }
        public long Element { get; private set; }
        public int Publisher { get; private set; }
        public int VerificationDigit { get; private set;}

        public ISBN (long element, int publisher)
        {
            GTIN = 978;
            Group = 65;
            Element = element;
            Publisher = publisher;
        }
    }
}
