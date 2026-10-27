namespace COMEX;

internal partial class Program
{
    private static readonly Dictionary<string, Cliente> clientes = new();
    private static readonly Dictionary<string, Produto> produtos = new(StringComparer.OrdinalIgnoreCase);
    private static readonly Dictionary<string, Carrinho> carrinhos = new();

    private static void Main()
    {
        CarregarDadosIniciais();

        while (ExibirMenuDeOpcoes() == 1)
        {
        }
    }
}
