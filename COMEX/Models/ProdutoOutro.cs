namespace COMEX;

internal class ProdutoOutro : Produto
{
    public override string Classificacao => "Outro";

    public ProdutoOutro(int id, string nome, decimal preco, string descricao = "", int quantidade = 0)
        : base(id, nome, preco, descricao, quantidade)
    {
    }
}
