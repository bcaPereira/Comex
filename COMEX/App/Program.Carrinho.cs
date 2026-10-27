namespace COMEX;

internal partial class Program
{
    private static void AdicionaProdutoNoCarrinho()
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
            Console.WriteLine($"• #{produto.Id} - {produto.Nome} | R$ {produto.Preço:N2} | Estoque: {produto.Quantidade}");
        }

        Console.WriteLine();
        string entradaProduto = LerTextoObrigatorio("Digite o nome ou ID do produto (ex: 1x45 ou Robalo x123):");

        if (!TentarInterpretarEntradaProduto(entradaProduto, out string seletorProduto, out int? quantidadeNaEntrada))
        {
            ExibirMensagem("Formato inválido. Use nome/ID ou nome/ID + x{quantidade}.", ConsoleColor.Red);
            return;
        }

        Produto? produtoSelecionado = BuscarProdutoPorNomeOuId(seletorProduto);

        if (produtoSelecionado is not null)
        {
            int quantidadeAdicionar = quantidadeNaEntrada ?? LerQuantidadeProduto();

            for (int i = 0; i < quantidadeAdicionar; i++)
            {
                carrinhos[cpf].Produtos.Add(produtoSelecionado);
            }

            ExibirMensagem($"{quantidadeAdicionar}x produto #{produtoSelecionado.Id} - {produtoSelecionado.Nome} adicionado ao carrinho de {carrinhos[cpf].Cliente.Nome}.", ConsoleColor.Green);
        }
        else
        {
            ExibirMensagem("Produto não encontrado.", ConsoleColor.Red);
        }
    }

    private static void ListarCarrinho()
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
                Console.WriteLine($"• #{produto.Id} - {produto.Nome} | R$ {produto.Preço:N2}");
            }
        }
        else
        {
            ExibirMensagem("Carrinho não encontrado para este cliente.", ConsoleColor.Red);
        }
    }

    private static void FechaCompra()
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

            var quantidadeFinalPorProduto = new Dictionary<string, int>(itensPorProduto, StringComparer.OrdinalIgnoreCase);

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
                    quantidadeFinalPorProduto[item.Nome] = item.EmEstoque;
                }

                string comprarDisponivel = LerOpcaoSimOuNao("Deseja comprar apenas a quantidade disponível? (S/N)");
                if (comprarDisponivel != "S")
                {
                    ExibirMensagem("Compra cancelada e retorno ao menu.", ConsoleColor.Yellow);
                    return;
                }
            }

            quantidadeFinalPorProduto = quantidadeFinalPorProduto
                .Where(item => item.Value > 0)
                .ToDictionary(item => item.Key, item => item.Value, StringComparer.OrdinalIgnoreCase);

            if (quantidadeFinalPorProduto.Count == 0)
            {
                ExibirMensagem("Nenhum item disponível para finalizar a compra.", ConsoleColor.Red);
                return;
            }

            int quantidadeItensFinal = quantidadeFinalPorProduto.Sum(item => item.Value);
            decimal totalFinal = quantidadeFinalPorProduto.Sum(item => produtos[item.Key].Preço * item.Value);

            if (totalFinal != total || quantidadeItensFinal != quantidadeItens)
            {
                ExibirMensagem($"Resumo ajustado para estoque disponível: {quantidadeItensFinal} item(ns) | Total R$ {totalFinal:N2}", ConsoleColor.Cyan);
            }

            foreach (var item in quantidadeFinalPorProduto)
            {
                produtos[item.Key].Quantidade -= item.Value;
            }

            string dadosCartao = LerTextoObrigatorio("Insira os dados do cartão:");
            Console.Clear();
            ExibirCabecalhoTela("Processando pagamento");
            Console.WriteLine($"Realizando pagamento de R$ {totalFinal:N2} no cartão {dadosCartao}.");
            Animacao();

            ExibirMensagem($"Pagamento de R$ {totalFinal:N2} realizado com sucesso para {carrinhos[cpf].Cliente.Nome} ({carrinhos[cpf].Cliente.CPF}).", ConsoleColor.Green);
            carrinhos.Remove(cpf);
        }
        else
        {
            ExibirMensagem("Compra cancelada.", ConsoleColor.Yellow);
        }
    }

    private static void ExibirResumoFechamento(string nome, string cpf, int quantidadeItens, decimal total)
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
}
