using BookManager.BookManager;
using BookManager.CreateBook;

internal class Program
{
    static Shelf shelf = new Shelf();
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
            Console.WriteLine("0 - Sair do programa");

            // Armazena posição escolhida se for válida
            Console.Write("Digite a opção: ");
            correct = int.TryParse(Console.ReadLine(), out opt);

            // Verificador de escolha
            if (opt < 0 || opt > 2) correct = false;
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
        switch(opt)
        {
            case 1:
                correct = CreateBook();
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

        Console.WriteLine("Digite o autor, se for mais de um, os separe com barra '|'. Exemplo: Fulano | Ciclano");
        string writer = Console.ReadLine();

        // Separa escritor por escritor
        string[] writers = SeparateWriters(writer);

        Book book = new(title, publisher, writers);
        if (!shelf.StoreBook(book))
        {
            Console.WriteLine("Livro já foi anteriormente registrado!");
            Console.WriteLine("Tecle ENTER para continuar");
            Console.ReadLine();
            return false;
        }
        return true;
    }

    private static bool VerifyBook(string title, string publisher)
    {
        return false;
    }

    private static string[] SeparateWriters(string writer)
    {
        // Separa os escritores pela barra
        string[] writers = writer.Split('|');

        // Retira os espaços em excesso
        for (int i = 0; i < writers.Length; i++)
        {
            writers[i] = writers[i].Trim();
        }

        return writers; 
    }
}