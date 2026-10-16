using System.Globalization;

class Program
{
    private static readonly Dictionary<string, Cliente> clientes = new();
    private static readonly Dictionary<string, Produto> produtos = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<string, Carrinho> carrinhos = new();

    static void Main()
    {
        CarregarDadosIniciais();

        while (ExibirMenuDeOpcoes() == 1)
        {
        }
    }

    static void CarregarDadosIniciais()
    {
        clientes.Clear();
        produtos.Clear();

        foreach (var cliente in DadosIniciais.ObterClientes())
        {
            clientes[cliente.CPF] = cliente;
        }

        foreach (var produto in DadosIniciais.ObterProdutos())
        {
            produtos[produto.Nome] = produto;
        }
    }

    static int ExibirMenuDeOpcoes()
    {
        Console.Clear();
        ExibirMenuPrincipal();
        int i = LerOpcaoMenu();

        switch (i)
        {
            case 1:
                Console.Clear();
                ExibirCabecalhoTela("Cadastrar cliente");
                CadastrarCliente();
                Console.Clear();
                break;
            case 2:
                Console.Clear();
                ExibirCabecalhoTela("Listar clientes");
                ListaClientes();
                Console.Clear();
                break;
            case 3:
                Console.Clear();
                ExibirCabecalhoTela("Cadastrar produto");
                CadastraProduto();
                Console.Clear();
                break;
            case 4:
                Console.Clear();
                ExibirCabecalhoTela("Ajustar preço de produto");
                AjustarPrecoDeProduto();
                Console.Clear();
                break;
            case 5:
                ListaProdutosPaginada();
                Console.Clear();
                break;
            case 6:
                Console.Clear();
                ExibirCabecalhoTela("Adicionar produto no carrinho");
                AdicionaProdutoNoCarrinho();
                Espera();
                Console.Clear();
                break;
            case 7:
                Console.Clear();
                ExibirCabecalhoTela("Listar carrinho");
                ListarCarrinho();
                Espera();
                Console.Clear();
                break;
            case 8:
                Console.Clear();
                ExibirCabecalhoTela("Fechar compra");
                FechaCompra();
                Espera();
                Console.Clear();
                break;
            case -1:
                Console.Clear();
                ExibirCabecalhoTela("Encerrando sistema");
                ExibirMensagem("COMEX FINALIZADO!", ConsoleColor.Yellow);
                Espera();
                return 0;
            default:
                ExibirMensagem("Opção inválida.", ConsoleColor.Red);
                break;
        }

        return 1;
    }

    static void CadastrarCliente()
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

    static void ListaClientes()
    {
        if (clientes.Count == 0)
        {
            ExibirMensagem("Nenhum cliente cadastrado.", ConsoleColor.Yellow);
            Espera();
            return;
        }

        foreach (var cliente in clientes.Values.OrderBy(c => c.Nome))
        {
            Console.WriteLine($"• Nome: {cliente.Nome} | CPF: {cliente.CPF}");
        }

        Espera();
    }

    static void CadastraProduto()
    {
        string nome = LerTextoObrigatorio("Digite o nome do produto:");

        if (produtos.ContainsKey(nome))
        {
            ExibirMensagem("Já existe um produto com esse nome.", ConsoleColor.Red);
            Espera();
            return;
        }

        decimal preco = LerDecimalValido("Digite o preço do produto:", 0);
        string descricao = LerTextoObrigatorio("Digite a descrição do produto:");
        int quantidade = LerInteiroValido("Digite a quantidade do produto:", 0);

        Produto p = new(nome, preco, descricao, quantidade);
        produtos.Add(nome, p);

        Console.Clear();
        ExibirCabecalhoTela("Cadastrar produto");
        ExibirMensagem($"Produto {p.Nome} (R$ {p.Preço:N2}) cadastrado com sucesso.", ConsoleColor.Green);
        Espera();
    }

    static void AjustarPrecoDeProduto()
    {
        string nome = LerTextoObrigatorio("Digite o nome do produto:");

        if (produtos.ContainsKey(nome))
        {
            decimal preco = LerDecimalValido("Digite o novo preço do produto:", 0);
            produtos[nome].Preço = preco;
            ExibirMensagem($"Preço do produto {nome} ajustado para R$ {preco:N2}.", ConsoleColor.Green);
        }
        else
        {
            ExibirMensagem($"Produto {nome} não encontrado.", ConsoleColor.Red);
        }

        Espera();
    }

    static void ListaProdutosPaginada()
    {
        if (produtos.Count == 0)
        {
            Console.Clear();
            ExibirCabecalhoTela("Listar produtos");
            ExibirMensagem("Nenhum produto cadastrado.", ConsoleColor.Yellow);
            Espera();
            return;
        }

        List<Produto> lista = produtos.Values.OrderBy(p => p.Nome).ToList();
        const int tamanhoPagina = 10;
        int paginaAtual = 0;
        int totalPaginas = (int)Math.Ceiling(lista.Count / (double)tamanhoPagina);

        while (true)
        {
            Console.Clear();
            ExibirCabecalhoTela("Listar produtos");
            Console.WriteLine($"Página {paginaAtual + 1} de {totalPaginas}");
            Console.WriteLine();
            Console.WriteLine("┌──────────────────────────────┬──────────────┬────────────┐");
            Console.WriteLine("│ Produto                      │      Preço   │ Quantidade │");
            Console.WriteLine("├──────────────────────────────┼──────────────┼────────────┤");

            foreach (var produto in lista.Skip(paginaAtual * tamanhoPagina).Take(tamanhoPagina))
            {
                string nome = LimitarTexto(produto.Nome, 28);
                Console.WriteLine($"│ {nome,-28} │ R$ {produto.Preço,8:N2} │ {produto.Quantidade,10} │");
            }

            Console.WriteLine("└──────────────────────────────┴──────────────┴────────────┘");
            Console.WriteLine();
            Console.WriteLine("[→ ou D] Próxima página  [← ou A] Página anterior  [S] Sair");

            var tecla = Console.ReadKey(true).Key;

            if ((tecla == ConsoleKey.RightArrow || tecla == ConsoleKey.D) && paginaAtual < totalPaginas - 1)
            {
                paginaAtual++;
                continue;
            }

            if ((tecla == ConsoleKey.LeftArrow || tecla == ConsoleKey.A) && paginaAtual > 0)
            {
                paginaAtual--;
                continue;
            }

            if (tecla == ConsoleKey.S || tecla == ConsoleKey.Escape || tecla == ConsoleKey.Enter)
            {
                break;
            }
        }
    }

    static void AdicionaProdutoNoCarrinho()
    {
        string cpf = LerTextoObrigatorio("Informe o CPF do cliente:");
        if (!clientes.ContainsKey(cpf))
        {
            ExibirMensagem("Usuário não cadastrado.", ConsoleColor.Red);
            return;
        }

        if (!carrinhos.ContainsKey(cpf))
        {
            carrinhos[cpf] = new Carrinho();
        }

        carrinhos[cpf].Cliente = clientes[cpf];
        Console.Clear();
        ExibirCabecalhoTela("Adicionar produto no carrinho");
        ExibirMensagem($"Cliente {carrinhos[cpf].Cliente.Nome} selecionado.", ConsoleColor.Cyan);
        Console.WriteLine();

        if (produtos.Count == 0)
        {
            ExibirMensagem("Não há produtos cadastrados.", ConsoleColor.Yellow);
            return;
        }

        Console.WriteLine("Produtos disponíveis:");
        foreach (var produto in produtos.Values.OrderBy(p => p.Nome))
        {
            Console.WriteLine($"• {produto.Nome} | R$ {produto.Preço:N2} | Estoque: {produto.Quantidade}");
        }

        Console.WriteLine();
        string nomeProduto = LerTextoObrigatorio("Digite o nome do produto que deseja adicionar ao carrinho:");

        if (produtos.ContainsKey(nomeProduto))
        {
            carrinhos[cpf].Produtos.Add(produtos[nomeProduto]);
            ExibirMensagem($"Produto {nomeProduto} adicionado ao carrinho de {carrinhos[cpf].Cliente.Nome}.", ConsoleColor.Green);
        }
        else
        {
            ExibirMensagem("Produto não encontrado.", ConsoleColor.Red);
        }
    }

    static void ListarCarrinho()
    {
        string cpf = LerTextoObrigatorio("Informe o CPF do cliente:");
        if (carrinhos.ContainsKey(cpf))
        {
            Console.Clear();
            ExibirCabecalhoTela("Listar carrinho");
            var carrinho = carrinhos[cpf];
            ExibirMensagem($"Carrinho do cliente {carrinho.Cliente.Nome}:", ConsoleColor.Cyan);
            Console.WriteLine();

            if (carrinho.Produtos.Count == 0)
            {
                ExibirMensagem("Carrinho vazio.", ConsoleColor.Yellow);
                return;
            }

            foreach (var produto in carrinho.Produtos)
            {
                Console.WriteLine($"• {produto.Nome} | R$ {produto.Preço:N2}");
            }
        }
        else
        {
            ExibirMensagem("Carrinho não encontrado para este cliente.", ConsoleColor.Red);
        }
    }

    static void FechaCompra()
    {
        string cpf = LerTextoObrigatorio("Informe o CPF do cliente:");
        if (!carrinhos.ContainsKey(cpf))
        {
            ExibirMensagem("Cliente não cadastrado.", ConsoleColor.Red);
            return;
        }

        var carrinho = carrinhos[cpf];
        if (carrinho.Produtos.Count == 0)
        {
            ExibirMensagem("Carrinho vazio. Adicione produtos antes de fechar a compra.", ConsoleColor.Yellow);
            return;
        }

        int quantidadeItens = carrinho.Produtos.Count;
        decimal total = carrinho.Produtos.Sum(p => p.Preço);

        ExibirResumoFechamento(carrinho.Cliente.Nome, carrinho.Cliente.CPF, quantidadeItens, total);

        string resposta = LerOpcaoSimOuNao("Deseja finalizar a compra? (S/N)");
        if (resposta == "S")
        {
            var itensPorProduto = carrinho.Produtos
                .GroupBy(p => p.Nome, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.Count(), StringComparer.OrdinalIgnoreCase);

            var produtosSemEstoque = itensPorProduto
                .Select(item => new
                {
                    Nome = item.Key,
                    Solicitado = item.Value,
                    EmEstoque = produtos.TryGetValue(item.Key, out var produtoEmEstoque) ? produtoEmEstoque.Quantidade : 0
                })
                .Where(item => item.Solicitado > item.EmEstoque)
                .ToList();

            if (produtosSemEstoque.Count > 0)
            {
                ExibirMensagem("Não foi possível finalizar a compra. Há produto(s) em falta no estoque:", ConsoleColor.Red);
                foreach (var item in produtosSemEstoque)
                {
                    Console.WriteLine($"• {item.Nome} | Em estoque: {item.EmEstoque} | Tentando comprar: {item.Solicitado}");
                }
                return;
            }

            foreach (var item in itensPorProduto)
            {
                produtos[item.Key].Quantidade -= item.Value;
            }

            string dadosCartao = LerTextoObrigatorio("Insira os dados do cartão:");
            Console.Clear();
            ExibirCabecalhoTela("Processando pagamento");
            Console.WriteLine($"Realizando pagamento de R$ {total:N2} no cartão {dadosCartao}.");
            Animacao();

            ExibirMensagem($"Pagamento de R$ {total:N2} realizado com sucesso para {carrinhos[cpf].Cliente.Nome} ({carrinhos[cpf].Cliente.CPF}).", ConsoleColor.Green);
            carrinhos.Remove(cpf);
        }
        else
        {
            ExibirMensagem("Compra cancelada.", ConsoleColor.Yellow);
        }
    }

    static void ExibirResumoFechamento(string nome, string cpf, int quantidadeItens, decimal total)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine("║               RESUMO PARA SEPARAÇÃO                 ║");
        Console.WriteLine("╠══════════════════════════════════════════════════════╣");
        Console.ResetColor();
        Console.WriteLine($"║ Cliente: {LimitarTexto(nome, 42),-42} ║");
        Console.WriteLine($"║ CPF: {LimitarTexto(cpf, 46),-46} ║");
        Console.WriteLine($"║ Quantidade de itens: {quantidadeItens,28} ║");
        Console.WriteLine($"║ Valor total: R$ {total,34:N2} ║");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void Espera()
    {
        Console.WriteLine("Aperte enter para continuar...");
        Console.ReadLine();
    }

    static int LerOpcaoMenu()
    {
        while (true)
        {
            Console.Write("Escolha uma opção: ");
            string? entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int opcao) && (opcao == -1 || (opcao >= 1 && opcao <= 8)))
            {
                return opcao;
            }

            Console.WriteLine("Opção inválida. Digite um número entre 1 e 8, ou -1 para sair.");
        }
    }

    static void ExibirMenuPrincipal()
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
        Console.WriteLine("║ -1  | Sair                                          ║");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();
    }

    static void ExibirCabecalhoTela(string titulo)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("╔══════════════════════════════════════════════════════╗");
        Console.WriteLine($"║ {titulo.PadRight(52)} ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void ExibirMensagem(string mensagem, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.WriteLine(mensagem);
        Console.ResetColor();
    }

    static string LerTextoObrigatorio(string mensagem)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine("Entrada inválida. O valor não pode ser vazio.");
        }
    }

    static int LerInteiroValido(string mensagem, int valorMinimo)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int valor) && valor >= valorMinimo)
            {
                return valor;
            }

            Console.WriteLine($"Entrada inválida. Digite um número inteiro maior ou igual a {valorMinimo}.");
        }
    }

    static decimal LerDecimalValido(string mensagem, decimal valorMinimo)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();

            bool valorAtual = decimal.TryParse(
                entrada,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out decimal valor);

            bool valorInvariante = decimal.TryParse(
                entrada,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal valorInvarianteConvertido);

            if (valorAtual && valor >= valorMinimo)
            {
                return valor;
            }

            if (valorInvariante && valorInvarianteConvertido >= valorMinimo)
            {
                return valorInvarianteConvertido;
            }

            Console.WriteLine($"Entrada inválida. Digite um número maior ou igual a {valorMinimo}.");
        }
    }

    static string LerOpcaoSimOuNao(string mensagem)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(entrada))
            {
                string valor = entrada.Trim().ToUpperInvariant();
                if (valor == "S" || valor == "N")
                {
                    return valor;
                }
            }

            Console.WriteLine("Entrada inválida. Digite S para sim ou N para não.");
        }
    }

    static string LimitarTexto(string texto, int tamanhoMaximo)
    {
        if (texto.Length <= tamanhoMaximo)
        {
            return texto;
        }

        return texto[..(tamanhoMaximo - 3)] + "...";
    }

    static void Animacao()
    {
        string[] etapas =
        {
            "Validando dados do pagamento",
            "Autorizando transação",
            "Finalizando compra"
        };

        char[] spinner = { '|', '/', '-', '\\' };
        int larguraBarra = 26;
        bool cursorVisivel = Console.CursorVisible;
        Console.CursorVisible = false;

        try
        {
            foreach (string etapa in etapas)
            {
                for (int progresso = 0; progresso <= 100; progresso += 4)
                {
                    int preenchido = progresso * larguraBarra / 100;
                    string barra = new string('█', preenchido) + new string('·', larguraBarra - preenchido);
                    char frame = spinner[(progresso / 4) % spinner.Length];

                    Console.Write($"\r{frame} {etapa} [{barra}] {progresso,3}%");
                    Thread.Sleep(55);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\r✔ ");
                Console.ResetColor();
                Console.WriteLine($"{etapa} [concluído]");
            }

            Console.WriteLine();
            ExibirMensagem("Compra processada com sucesso!", ConsoleColor.Green);
        }
        finally
        {
            Console.CursorVisible = cursorVisivel;
        }
    }
}

class Produto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
    public decimal Preço { get; set; }

    public Produto(string nome, decimal preco, string descricao = "", int quantidade = 0)
    {
        Nome = nome;
        Descricao = descricao;
        Quantidade = quantidade;
        Preço = preco;
    }
}

class Cliente
{
    public string Nome { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Profissao { get; set; } = string.Empty;
}

class Carrinho
{
    public List<Produto> Produtos { get; set; } = new();
    public Cliente Cliente { get; set; } = new();
}
