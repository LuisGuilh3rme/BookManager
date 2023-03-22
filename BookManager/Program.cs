using BookManager.BookManager;
using BookManager.CreateBook;

internal class Program
{
    static Shelf shelf = new();

    private static void Main(string[] args)
    {
        int opt = 0;
        do
        {
            opt = Menu();
            if (opt == 0) Environment.Exit(0);

        } while (BookManagement(opt));
    }

    private static int Menu()
    {
        // Declaração de variáveis
        bool correct = false;
        int opt = 0;

        // Inicialização do menu
        do
        {

            Console.Clear();
            Console.WriteLine("Menu de opções: ");
            Console.WriteLine("1 - Adicionar livro");
            Console.WriteLine("2 - Listar livros");
            Console.WriteLine("3 - Remover livros");
            Console.WriteLine("4 - Trocar Status");
            Console.WriteLine("0 - Sair do programa");

            // Armazena posição escolhida se for válida
            Console.Write("Digite a opção: ");
            correct = int.TryParse(Console.ReadLine(), out opt);

            // Verificador de escolha
            if (opt < 0 || opt > 3) correct = false;
            if (!correct)
            {
                Console.WriteLine("Opção Inválida");
                Console.WriteLine("Tecle ENTER para continuar");
                Console.ReadLine();
            }

        } while (!correct);

        return opt;
    }

    private static bool BookManagement(int opt)
    {
        bool correct = false;

        // Menu de escolhas do usuário
        switch(opt)
        {
            case 1:
                correct = CreateBook();
                break;
            case 2:
                correct = PrintShelf();
                break;
            case 3:
                correct = RemoveBook();
                break;
        }

        Console.WriteLine();

        Console.WriteLine("Deseja continuar? Digite N para PARAR | Qualquer outra tecla para CONTINUAR: ");
        return (Console.ReadLine().ToUpper() == "N") ? false: true;
    }

    private static bool CreateBook()
    {
        Console.Clear();
        Console.WriteLine("**ADICIONAR LIVRO**\n\n");

        Console.Write("Título do livro: ");
        string title = Console.ReadLine();

        Console.Write("Editora: ");
        string publisher = Console.ReadLine();

        // Verificar se livro já não está registrado
        if (VerifyBook(title, publisher))
        {
            Console.WriteLine("Livro já foi anteriormente registrado!");
            Console.WriteLine("Tecle ENTER para continuar");
            Console.ReadLine();
            return false;
        }

        ISBN isbn = CreateISBN(publisher);
        if (isbn == null)
        {
            Console.WriteLine("Impossível gerar ISBN");
            Console.WriteLine("Tecle ENTER para continuar");
            Console.ReadLine();
            return false;
        }

        Console.WriteLine("Digite o autor, se for mais de um, os separe com barra '|'. Exemplo: Fulano | Ciclano");
        string writers = Console.ReadLine();

        // Cria livro
        Book book = new(title, publisher, writers, isbn);

        // Verificar se livro já não está registrado
        if (!shelf.StoreBook(book))
        {
            Console.WriteLine("Livro já foi anteriormente registrado!");
            Console.WriteLine("Tecle ENTER para continuar");
            Console.ReadLine();
            return false;
        }

        return true;
    }

    private static bool PrintShelf()
    {
        // Verifica tamanho da estante
        if (shelf.ShelfCount() == 0)
        {
            Console.WriteLine("Estante vazia!");
            Console.WriteLine("Tecle ENTER para continuar");
            Console.ReadLine();
            return false;
        }

        // Exibe cada livro na estante
        int count = 0;
        for (int i = 0; i < shelf.ShelfCount(); i++)
        {
            Console.Write("{0})", ++count);
            Console.WriteLine(shelf.Books[i]);
        }
        return true;
    }

    private static bool RemoveBook()
    {
        if (!PrintShelf()) return false;

        int index = 0;
        {
            Console.Write("Insira o index do livro que quer retirar: ");
            int.TryParse(Console.ReadLine(), out index);
        } while (index == 0);

        shelf.RemoveBook(index);
        return true;
    }

    private static ISBN CreateISBN(string publisher)
    {
        string publisherTag = shelf.FindPublisher(publisher);
        Random rnd = new Random();

        // Cria elemento registrante
        string regElemnt = "";
        for (int i = 0; i < 5; i++)
        {
            regElemnt += rnd.Next(10);
        }

        // Cria ISBN
        ISBN isbn = new ISBN(regElemnt, publisherTag);

        // Verifica se já existe ou não
        if (shelf.FindBook(isbn)) return null;
        return isbn;
    }

    private static bool VerifyBook(string title, string publisher)
    {
        return shelf.FindBook(title, publisher);
    }
}