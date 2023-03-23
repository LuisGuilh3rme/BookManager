using System.Linq.Expressions;
using BookManager.CreateBook;

namespace BookManager.BookManager
{
    internal class FileManager
    {
        static string Path = @"C:\Users\" + Environment.UserName + @"\Documents\";
        string FullPath { get; set; }
        private StreamWriter _sw { get; set; }
        private StreamReader _sr { get; set; }

        public FileManager()
        {
            FullPath = Path + "estante.txt";
        }

        public bool StoreItem(string item)
        {
            _sw = File.AppendText(FullPath);

            // Tenta armazenar item
            try
            {
                _sw.WriteLine(item);
                _sw.Close();
            }
            catch (Exception ex)
            {
                // Em caso de erro, avisar e retornar falso
                Console.WriteLine("Erro encontrado: " + ex.Message);
                return false;
            }

            return true;
        }

        public bool FileExists()
        {
            return File.Exists(@"backup.ebm");
        }

        public bool WriteBackup(List<Book> books)
        {
            _sw = new StreamWriter(@"backup.ebm");

            for (int i = 0; i < books.Count; i++)
            {
                _sw.WriteLine(books[i].BackupString());
            }

            _sw.Close();
            return true;
        }

        public bool OverWrite(List<Book> books)
        {
            if (!WriteBackup(books)) return false;

            // Sobrescrever informações
            _sw = new StreamWriter(FullPath);

            _sw.WriteLine("**Livros Guardados**");
            _sw.Close();

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Status == "Guardado") StoreItem(books[i].ToString());
            }

            _sw = File.AppendText(FullPath);
            _sw.WriteLine();
            _sw.WriteLine("**Livros Emprestados**");
            _sw.Close();

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Status == "Emprestado") StoreItem(books[i].ToString());
            }

            return true;
        }

        public List<Book> ReturnInformation()
        {
            List<Book> books = new List<Book>();

            // Tenta ler arquivo
            try
            {
                _sr = new StreamReader(@"backup.ebm");
            }
            catch (Exception ex)
            {
                // Em caso de erro, avisar e retornar nulo
                Console.WriteLine("Erro encontrado: " + ex.Message);
                return null;
            }

            // Armazena linha por linha cada livro
            string line = _sr.ReadLine();

            while (line != null)
            {
                string[] aux = line.Split('|');

                string title = aux[0];
                string publisher = aux[1];
                string writers = aux[2];
                string isbn = aux[3];
                string status = aux[4];

                books.Add(new Book(title, publisher, writers, new ISBN(isbn), status));
                line = _sr.ReadLine();
            }
            _sr.Close();

            return books;
        }

    }
}
