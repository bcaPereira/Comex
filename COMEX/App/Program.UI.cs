namespace COMEX;

internal partial class Program
{
    private static void Espera()
    {
        Console.WriteLine("Aperte enter para continuar...");
        Console.ReadLine();
    }

    private static int LerOpcaoMenu()
    {
        while (true)
        {
            Console.Write("Escolha uma opção: ");
            string? entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int opcao) && (opcao == -1 || (opcao >= 1 && opcao <= 9)))
            {
                return opcao;
            }

            Console.WriteLine("Opção inválida. Digite um número entre 1 e 8, ou -1 para sair.");
        }
    }

    private static void ExibirMenuPrincipal()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    COMEX - MENU                     ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════╣");
        Console.ResetColor();

        Console.WriteLine("║  1  | Cadastrar cliente                             ║");
        Console.WriteLine("║  2  | Listar clientes                               ║");
        Console.WriteLine("║  3  | Cadastrar produto                             ║");
        Console.WriteLine("║  4  | Ajustar preço de produto                      ║");
        Console.WriteLine("║  5  | Listar produtos                               ║");
        Console.WriteLine("║  6  | Adicionar produto no carrinho                 ║");
        Console.WriteLine("║  7  | Listar carrinho                               ║");
        Console.WriteLine("║  8  | Fechar compra                                 ║");
        Console.WriteLine("║  9  | CARREGAR DA API                               ║");
        Console.WriteLine("║ -1  | Sair                                          ║");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    private static void ExibirCabecalhoTela(string titulo)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine($"║ {titulo.PadRight(52)} ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    private static void ExibirMensagem(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }
}
