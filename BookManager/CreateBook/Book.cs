﻿using System;
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
        public string Writers { get; private set; }
        public string InlineWriters { get; private set; }

        public string Status { get; private set; }
        public ISBN ISBN { get; private set; }

        public Book(string title, string publisher, string writers, ISBN isbn, string status = "Guardado")
        {
            Title = title;
            Publisher = publisher;
            Writers = writers;
            SeparateWriters();
            Status = status;
            ISBN = isbn;
        }

        public void ChangeStatus()
        {
            // Troca o status do livro
            if (Status == "Guardado") Status = "Emprestado";
            else Status = "Guardado";
        }

        private string SeparateWriters()
        {
            InlineWriters = "";
            // Separa os escritores pela barra
            string[] writersArray = Writers.Split('|');

            // Retira os espaços em excesso
            for (int i = 0; i < writersArray.Length; i++)
            {
                writersArray[i] = writersArray[i].Trim();
            }

            // Transforma novamente em string
            for (int i = 0; i < writersArray.Length; i++)
            {
                InlineWriters += writersArray[i];
                if (i != writersArray.Length - 1) InlineWriters += " & ";
            }

            return InlineWriters;
        }

        public string BackupString()
        {
            return $"{Title}|{Publisher}|{InlineWriters}|{ISBN}|{Status}";
        }

        public override string ToString()
        {
            return $"Título: {Title} | Editora: {Publisher} | Autores: {InlineWriters} | ISBN: {ISBN} | Status: {Status}";
        }
    }
}
