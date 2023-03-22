using BookManager.CreateBook;

namespace BookManager.BookManager
{
    internal class Shelf
    {
        public List<Book> books;

        public Shelf()
        {
            books = new List<Book>();
        }

        public bool StoreBook(Book book)
        {
            // Verifica se por algum motivo o livro já foi registrado
            if (books.IndexOf(book) != -1) return false;

            books.Add(book);
            return true;
        }
    }
}
