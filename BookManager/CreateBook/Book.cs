using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.CreateBook
{
    internal class Book
    {
        public string Title { get; private set; }
        public string Publisher { get; private set; }
        public string[] Writers { get; private set; }
        public ISBN ISBN { get; private set; }

        public Book(string title, string publisher, string[] writers)
        {
            Title = title;
            Publisher = publisher;
            Writers = writers;
        }

        private string AllWriters()
        {
            string writers = "";
            for (int i = 0; i < Writers.Length; i++)
            {
                writers += Writers[i];
                if (i != Writers.Length - 1) writers += " & ";
            }
            return writers;
        }

        public override string ToString()
        {
            return $"Título: {Title} | Editora: {Publisher} | Autores: {AllWriters()} | ISBN: NENHUM";
        }

    }
}
