﻿using BookManager.BookManager;
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
            Console.WriteLine("5 - Ordenar arquivos");
            Console.WriteLine("0 - Sair do programa");

            // Armazena posição escolhida se for válida
            Console.Write("Digite a opção: ");
            correct = int.TryParse(Console.ReadLine(), out opt);

            // Verificador de escolha
            if (opt < 0 || opt > 5) correct = false;
            if (!correct)
            {
                PrintError("Opção Inválida");
                Console.WriteLine("Aperte ENTER para continuar");
                Console.ReadLine();
            }

        } while (!correct);

        return opt;
    }

    private static bool BookManagement(int opt)
    {
        bool correct = false;

        Console.WriteLine();

        // Menu de escolhas do usuário
        switch (opt)
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
            case 4:
                correct = ChangeStatus();
                break;
            case 5:
                correct = OrderShelf();
                break;
        }

        Console.WriteLine();

        if (correct) Console.WriteLine("Sucesso! Digite N para PARAR | Tecle ENTER para CONTINUAR: ");
        else Console.WriteLine("Digite N para PARAR | ENTER para tentar novamente");
        return (Console.ReadLine().ToUpper() == "N") ? false: true;
    }

    private static bool CreateBook()
    {
        Console.WriteLine("**ADICIONAR LIVRO**\n\n");

        Console.Write("Título do livro: ");
        string title = Console.ReadLine();

        Console.Write("Editora: ");
        string publisher = Console.ReadLine();

        // Verificar se livro já não está registrado
        if (VerifyBook(title, publisher))
        {
            PrintError("Livro já foi anteriormente registrado!");
            return false;
        }

        ISBN isbn = CreateISBN(publisher);
        if (isbn == null)
        {
            PrintError("Impossível gerar ISBN");
            return false;
        }

        Console.WriteLine("Digite o autor, se for mais de um, os separe com barra '|'. Exemplo: Fulano | Ciclano");
        string writers = Console.ReadLine();

        // Cria livro
        Book book = new(title, publisher, writers, isbn);

        // Verificar se livro já não está registrado
        if (!shelf.StoreBook(book))
        {
            PrintError("Livro já foi anteriormente registrado!");
            return false;
        }

        return true;
    }

    private static bool PrintShelf()
    {
        Console.WriteLine("**EXIBIR ESTANTE**\n\n");
        // Verifica tamanho da estante
        if (shelf.ShelfCount() == 0)
        {
            PrintError("Estante vazia!");
            return false;
        }

        Console.WriteLine("Local de armazenamento: Documents\\estante.txt");

        // Exibe cada livro na estante
        int count = 0;
        for (int i = 0; i < shelf.ShelfCount(); i++)
        {
            Console.Write("{0}) ", ++count);
            Console.WriteLine(shelf.Books[i]);
        }
        return true;
    }

    private static bool RemoveBook()
    {
        Console.WriteLine("**REMOVER LIVRO**\n\n");
        // Imprime ou retorna se estante está vazia
        if (!PrintShelf()) return false;

        int index = 0;
        do{
            Console.Write("Insira o index do livro que quer retirar: ");
            int.TryParse(Console.ReadLine(), out index);
        } while (index == 0);

        // Procura e remove o livro pelo index
        if (!shelf.RemoveBook(index))
        {
            PrintError("Livro não encontrado!");
            return false;
        }
        return true;
    }

    private static bool ChangeStatus()
    {
        Console.WriteLine("**TROCAR STATUS DO LIVRO**\n\n");
        // Imprime ou retorna se estante está vazia
        if (!PrintShelf()) return false;

        int index = 0;
        do {
            Console.Write("Insira o index do livro que quer alterar o status: ");
            int.TryParse(Console.ReadLine(), out index);
        } while (index == 0) ;

        // Procura e altera o status do livro pelo index
        if (!shelf.ChangeBookStatus(index)) {
            PrintError("Livro não encontrado!");
            return false;
        }
        return true;
    }

    private static bool OrderShelf()
    {
        Console.WriteLine("**ORDENAR ESTANTE**\n\n");
        if (shelf.ShelfCount() == 0)
        {
            PrintError("Estante vazia!");
            return false;
        }

        int opt = 0;
        do
        {
            Console.WriteLine("1 - Ordenar por TITULO");
            Console.WriteLine("2 - Ordenar por AUTOR");
            Console.WriteLine("3 - Ordenar por STATUS");
            int.TryParse(Console.ReadLine(), out opt);
        } while (opt == 0);

        if (!shelf.Order(opt))
        {
            PrintError("Impossível ordenar lista");
            return false;
        }
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

    private static void PrintError(string error)
    {
        // Aviso de erro personalizado
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ForegroundColor = aux;
    }
}