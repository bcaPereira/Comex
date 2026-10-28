namespace COMEX;

internal partial class Program
{
    private static void CarregarDadosIniciais()
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

    private static int ObterProximoIdProduto()
    {
        if (produtos.Count == 0)
        {
            return 1;
        }

        return produtos.Values.Max(p => p.Id) + 1;
    }

    private static int ExibirMenuDeOpcoes()
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
            case 9:
                Console.Clear();
                CarregarDadosAsync().Wait();
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
}
