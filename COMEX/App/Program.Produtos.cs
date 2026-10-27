namespace COMEX;

internal partial class Program
{
    private static void CadastraProduto()
    {
        string nomeDigitado = LerTextoObrigatorio("Digite o nome do produto:");
        string nome = NormalizarNomeProduto(nomeDigitado);

        if (produtos.ContainsKey(nome))
        {
            ExibirMensagem("Já existe um produto com esse nome.", ConsoleColor.Red);
            Espera();
            return;
        }

        decimal preco = LerDecimalValido("Digite o preço do produto:", 0);
        string descricao = LerTextoObrigatorio("Digite a descrição do produto:");
        int quantidade = LerInteiroValido("Digite a quantidade do produto:", 0);
        string tipoProduto = LerTextoObrigatorio("Digite a classe do produto (Livro/Eletronico/Outro):");

        int id = ObterProximoIdProduto();
        Produto p = CriarProdutoPorClasse(id, nome, preco, descricao, quantidade, tipoProduto);
        produtos.Add(nome, p);

        Console.Clear();
        ExibirCabecalhoTela("Cadastrar produto");
        ExibirMensagem($"Produto #{p.Id} - {p.Nome} ({p.Classificacao}) (R$ {p.Preço:N2}) cadastrado com sucesso.", ConsoleColor.Green);
        Espera();
    }

    private static Produto CriarProdutoPorClasse(int id, string nome, decimal preco, string descricao, int quantidade, string tipoProduto)
    {
        if (tipoProduto.Equals("Eletronico", StringComparison.OrdinalIgnoreCase))
        {
            int voltagem = LerInteiroValido("Informe a voltagem do eletrônico:", 1);
            int potencia = LerInteiroValido("Informe a potência do eletrônico:", 1);
            return new ProdutoEletronico(id, nome, preco, voltagem, potencia, descricao, quantidade);
        }

        if (tipoProduto.Equals("Livro", StringComparison.OrdinalIgnoreCase))
        {
            string isbn = LerTextoObrigatorio("Informe o ISBN do livro:");
            int totalPaginas = LerInteiroValido("Informe o total de páginas:", 1);
            return new ProdutoLivro(id, nome, preco, isbn, totalPaginas, descricao, quantidade);
        }

        return new ProdutoOutro(id, nome, preco, descricao, quantidade);
    }

    private static void AjustarPrecoDeProduto()
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

    private static void ListaProdutosPaginada()
    {
        if (produtos.Count == 0)
        {
            Console.Clear();
            ExibirCabecalhoTela("Listar produtos");
            ExibirMensagem("Nenhum produto cadastrado.", ConsoleColor.Yellow);
            Espera();
            return;
        }

        int indiceOrdenacao = 0;
        bool maiorParaMenor = true;
        const int tamanhoPagina = 10;
        int paginaAtual = 0;

        while (true)
        {
            List<Produto> lista = OrdenarProdutos(produtos.Values.ToList(), indiceOrdenacao, maiorParaMenor);
            int totalPaginas = (int)Math.Ceiling(lista.Count / (double)tamanhoPagina);

            if (paginaAtual >= totalPaginas)
            {
                paginaAtual = Math.Max(0, totalPaginas - 1);
            }

            string criterio = ObterNomeOrdenacaoProduto(indiceOrdenacao);
            string direcao = maiorParaMenor ? "Maior para Menor" : "Menor para Maior";

            Console.Clear();
            ExibirCabecalhoTela("Listar produtos");
            Console.WriteLine($"Página {paginaAtual + 1} de {totalPaginas}");
            Console.WriteLine($"Ordenação: {criterio} ({direcao})");
            Console.WriteLine();
            Console.WriteLine("┌──────┬──────────────────────────────┬──────────────┬────────────┬────────────┐");
            Console.WriteLine("│  ID  │ Produto                      │      Preço   │ Quantidade │ Classe     │");
            Console.WriteLine("├──────┼──────────────────────────────┼──────────────┼────────────┼────────────┤");

            foreach (var produto in lista.Skip(paginaAtual * tamanhoPagina).Take(tamanhoPagina))
            {
                string nome = LimitarTexto(produto.Nome, 28);
                Console.WriteLine($"│ {produto.Id,4} │ {nome,-28} │ R$ {produto.Preço,8:N2} │ {produto.Quantidade,10} │ {produto.Classificacao,-10} │");
            }

            Console.WriteLine("└──────┴──────────────────────────────┴──────────────┴────────────┴────────────┘");
            Console.WriteLine();
            Console.WriteLine("[→ ou D] Próxima página  [← ou A] Página anterior");
            Console.WriteLine("[↑ ou W] Próximo critério (Maior→Menor)  [↓ ou S] Próximo critério (Menor→Maior)");
            Console.WriteLine("[Enter ou Espaço] Sair");

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

            if (tecla == ConsoleKey.UpArrow || tecla == ConsoleKey.W)
            {
                maiorParaMenor = true;
                indiceOrdenacao = (indiceOrdenacao + 1) % 4;
                paginaAtual = 0;
                continue;
            }

            if (tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.S)
            {
                maiorParaMenor = false;
                indiceOrdenacao = (indiceOrdenacao + 1) % 4;
                paginaAtual = 0;
                continue;
            }

            if (tecla == ConsoleKey.Enter || tecla == ConsoleKey.Spacebar)
            {
                break;
            }
        }
    }

    private static List<Produto> OrdenarProdutos(List<Produto> lista, int indiceOrdenacao, bool maiorParaMenor)
    {
        return indiceOrdenacao switch
        {
            0 => maiorParaMenor ? lista.OrderByDescending(p => p.Id).ToList() : lista.OrderBy(p => p.Id).ToList(),
            1 => maiorParaMenor ? lista.OrderByDescending(p => p.Nome).ToList() : lista.OrderBy(p => p.Nome).ToList(),
            2 => maiorParaMenor ? lista.OrderByDescending(p => p.Preço).ToList() : lista.OrderBy(p => p.Preço).ToList(),
            3 => maiorParaMenor ? lista.OrderByDescending(p => p.Quantidade).ToList() : lista.OrderBy(p => p.Quantidade).ToList(),
            _ => lista.OrderBy(p => p.Id).ToList()
        };
    }

    private static string ObterNomeOrdenacaoProduto(int indiceOrdenacao)
    {
        return indiceOrdenacao switch
        {
            0 => "ID",
            1 => "Nome",
            2 => "Preço",
            3 => "Quantidade",
            _ => "ID"
        };
    }
}
