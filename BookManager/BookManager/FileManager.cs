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
            string[] aux;

            while (line != null)
            {
                // Pula informações que não sejam livros:
                if (line.Contains("LIVROS EMPRESTADOS") || line.Contains("LIVROS ARMAZENADOS")) line = _sr.ReadLine();

                // Armazena informações em uma array
                aux = line.Split('|');

                // Cria uma array auxiliar com informações de cada atributo do objeto
                string[] objectCreator = new string[aux.Length];

                for (int i = 0; i < aux.Length; i++)
                {
                    aux[i] = aux[i].Trim();

                    // Pega a primeira aparição do ':'
                    int doubleDot = aux[i].IndexOf(':');

                    // Armazena tudo após ele
                    string objectAux = "";
                    for (int j = doubleDot + 1; j < aux[i].Length; j++)
                    {
                        objectAux += aux[i][j];
                    }
                    objectCreator[i] = objectAux.Trim();
                }
                books.Add(new Book(objectCreator[0], objectCreator[1], objectCreator[2]));

                line = _sr.ReadLine();
            }
            _sr.Close();
            return books;
        }
    }
}
