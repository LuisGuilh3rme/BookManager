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
            // Verifica se arquivo já existe ou deve ser criado
            if (FileExists()) _sw = File.AppendText(FullPath);
            else _sw = new StreamWriter(FullPath);

            // Tenta armazenar item
            try 
            {
                _sw.WriteLine(item);
                _sw.Close();
            } catch (Exception ex)
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

        public List<Book> ReturnInformation()
        {
            List<Book> books = new List<Book>();

            // Tenta ler arquivo
            try
            {
                _sr = new StreamReader(FullPath);
            } catch (Exception ex)
            {
                // Em caso de erro, avisar e retornar nulo
                Console.WriteLine("Erro encontrado: " + ex.Message);
                return null;
            }

            // Armazena linha por linha cada livro
            string line = _sr.ReadLine();
            while (line != null)
            {
                // Pula informações que não sejam livros:
                if (line.Contains("LIVROS EMPRESTADOS") || line.Contains("LIVROS ARMAZENADOS")) line = _sr.ReadLine();

                // Converte o ToString para tipo livro
                books.Add(new Book("teste", "teste", null));
                line = _sr.ReadLine();
            }

            _sr.Close();
            return books;
        }
    }
}
