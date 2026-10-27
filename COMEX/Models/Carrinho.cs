namespace COMEX;

internal class Carrinho
{
    public List<Produto> Produtos { get; set; } = new();
    public Cliente Cliente { get; set; } = new();
}
