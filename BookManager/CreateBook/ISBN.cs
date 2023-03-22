using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookManager.CreateBook
{
    internal class ISBN
    {
        public string GTIN { get; private set; }
        public string Group { get; private set; }
        public string Element { get; private set; }
        public string Publisher { get; private set; }
        public string VerificationDigit { get; private set;}

        public ISBN (string element, string publisher)
        {
            GTIN = "978";
            Group = "65";
            Element = element;
            Publisher = publisher;
            VerificationDigit = $"{new Random().Next(10)}";
        }

        public ISBN (string fullISBN)
        {
            // Separa uma string que contém ISBN completo e divide as informações entre os atributos
            string[] isbnSeparator = fullISBN.Split('-');
            GTIN = isbnSeparator[0];
            Group = isbnSeparator[1];
            Element = isbnSeparator[2];
            Publisher = isbnSeparator[3];
            VerificationDigit = isbnSeparator[4];
        }

        public override string ToString()
        {
            return $"{GTIN}-{Group}-{Element}-{Publisher}-{VerificationDigit}";
        }
    }
}
