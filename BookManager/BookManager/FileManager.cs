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
            return File.Exists(FullPath);
        }

        public bool OverWrite(List<Book> books)
        {
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
                _sr = new StreamReader(FullPath);
            }
            catch (Exception ex)
            {
                // Em caso de erro, avisar e retornar nulo
                Console.WriteLine("Erro encontrado: " + ex.Message);
                return null;
            }

            // Armazena linha por linha cada livro
            string line = _sr.ReadLine();
            string[] aux;

            while (line != null)
            {
                // Verifica se linha não é nula ou que não é um formato válido
                while (line != null && line == "")
                {
                    line = _sr.ReadLine();
                }

                if (line == null) break;

                // Verifica se linha não é nula ou que não é um formato válido
                while (line != null && line.Split('|').Length != 5)
                {
                    line = _sr.ReadLine();
                }

                if (line == null) break;

                    // Armazena informações em uma array
                    aux = line.Split('|');

                    // Cria uma array auxiliar com informações de cada atributo do objeto
                    string[] objectCreator = new string[aux.Length];

                    for (int i = 0; i < aux.Length; i++)
                    {
                        aux[i] = aux[i].Trim();

                        // Pega a primeira aparição do ':'
                        int colon = aux[i].IndexOf(':');

                        // Armazena tudo após ele
                        objectCreator[i] = aux[i].Substring(colon + 1).Trim();
                    }

                    // Cria um novo objeto com a array auxiliar e armazena na lista
                    books.Add(new Book(objectCreator[0], objectCreator[1], objectCreator[2], new ISBN(objectCreator[3]), objectCreator[4]));
                line = _sr.ReadLine();
            }
            _sr.Close();

            return books;
        }

    }
}
