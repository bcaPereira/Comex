namespace COMEX;

internal partial class Program
{
    private static void CadastrarCliente()
    {
        string nome = LerTextoObrigatorio("Digite o nome do cliente:");
        string cpf = LerTextoObrigatorio("Digite o CPF do cliente:");

        if (clientes.ContainsKey(cpf))
        {
            ExibirMensagem("Já existe cliente cadastrado com esse CPF.", ConsoleColor.Red);
            Espera();
            return;
        }

        Cliente cliente = new()
        {
            Nome = nome,
            CPF = cpf
        };

        clientes.Add(cpf, cliente);
        Console.Clear();
        ExibirCabecalhoTela("Cadastrar cliente");
        ExibirMensagem($"Cliente {cliente.Nome} ({cliente.CPF}) cadastrado com sucesso.", ConsoleColor.Green);
        Espera();
    }

    private static void ListaClientes()
    {
        if (clientes.Count == 0)
        {
            ExibirMensagem("Nenhum cliente cadastrado.", ConsoleColor.Yellow);
            Espera();
            return;
        }

        int indiceOrdenacao = 0;
        bool maiorParaMenor = true;

        while (true)
        {
            Console.Clear();
            ExibirCabecalhoTela("Listar clientes");

            List<Cliente> lista = OrdenarClientes(clientes.Values.ToList(), indiceOrdenacao, maiorParaMenor);
            string criterio = indiceOrdenacao == 0 ? "Nome" : "CPF";
            string direcao = maiorParaMenor ? "Maior para Menor" : "Menor para Maior";

            Console.WriteLine($"Ordenação: {criterio} ({direcao})");
            Console.WriteLine();

            foreach (var cliente in lista)
            {
                Console.WriteLine($"• Nome: {cliente.Nome} | CPF: {cliente.CPF}");
            }

            Console.WriteLine();
            Console.WriteLine("[↑ ou W] Próximo critério (Maior→Menor)  [↓ ou S] Próximo critério (Menor→Maior)");
            Console.WriteLine("[Enter ou Espaço] Sair");

            var tecla = Console.ReadKey(true).Key;

            if (tecla == ConsoleKey.UpArrow || tecla == ConsoleKey.W)
            {
                maiorParaMenor = true;
                indiceOrdenacao = (indiceOrdenacao + 1) % 2;
                continue;
            }

            if (tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.S)
            {
                maiorParaMenor = false;
                indiceOrdenacao = (indiceOrdenacao + 1) % 2;
                continue;
            }

            if (tecla == ConsoleKey.Enter || tecla == ConsoleKey.Spacebar)
            {
                break;
            }
        }
    }

    private static List<Cliente> OrdenarClientes(List<Cliente> lista, int indiceOrdenacao, bool maiorParaMenor)
    {
        return indiceOrdenacao switch
        {
            0 => maiorParaMenor ? lista.OrderByDescending(c => c.Nome).ToList() : lista.OrderBy(c => c.Nome).ToList(),
            1 => maiorParaMenor ? lista.OrderByDescending(c => c.CPF).ToList() : lista.OrderBy(c => c.CPF).ToList(),
            _ => lista.OrderBy(c => c.Nome).ToList()
        };
    }
}
