using BookManager.CreateBook;

namespace BookManager.BookManager
{
    internal class Shelf
    {
        public List<Book> Books { get; private set; }
        public FileManager fm { get; private set; }

        public Shelf()
        {
            fm = new();
            Books = SyncShelf();
        }

        public bool StoreBook(Book book)
        {
            // Verifica se por algum motivo o livro já foi registrado
            if (Books.IndexOf(book) != -1) return false;

            Books.Add(book);
            fm.OverWrite(Books);

            return true;
        }

        public bool RemoveBook(int index)
        {
            if (Books.Count < index) return false;
            Books.RemoveAt(index - 1);

            fm.OverWrite(Books);
            return true;
        }

        public bool ChangeBookStatus(int index)
        {
            if (Books.Count < index) return false;
            Books[index - 1].ChangeStatus();

            fm.OverWrite(Books);
            return true;
        }

        public int ShelfCount()
        {
            Books = SyncShelf();
            return Books.Count;
        }

        public List<Book> SyncShelf()
        {
            // Se não existir um arquivo de armazenamento, criar nova lista
            if (!fm.FileExists()) return new List<Book>();

            return fm.ReturnInformation();
        }

        public bool FindBook(string title, string publisher)
        {
            // Cria uma lista com livros com mesmo titulo
            List<Book> sameNameBooks = Books.FindAll(book => book.Title == title);

            // Retorna se existe algum com a mesma editora
            return sameNameBooks.Any(book => book.Publisher == publisher);
        }

        public bool FindBook(ISBN isbn)
        {
            return Books.Any(book => book.ISBN == isbn);
        }

        public string FindPublisher(string publisher)
        {
            // Procura o dígito da editora caso existente
            if (Books.Any(book => book.Publisher == publisher))
            {
                return Books.Find(book => book.Publisher == publisher).ISBN.Publisher;
            }

            // Cria um digito para a editora
            Random rdn = new Random();
            string publisherTag = "";
            for (int i = 0; i < 2; i++)
            {
                publisherTag += rdn.Next(10);
            }
            return publisherTag;
        }

        public bool Order(int opt)
        {
            // Opções de ordenação

            // Ordenar por titulo
            if (opt == 1) Books = Books.OrderBy(book => book.Title).ToList();

            // Ordenar por autor
            else if (opt == 2) Books = Books.OrderBy(book => book.InlineWriters).ToList();

            // Ordenar por status 
            else if (opt == 3) Books = Books.OrderBy(book => book.Status).ToList();

            else return false;

            fm.OverWrite(Books);
            return true;
        }
    }
}
