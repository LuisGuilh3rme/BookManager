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
            fm.StoreItem(book.ToString());

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
        //public bool FindBook(string isbn) { }
    }
}
