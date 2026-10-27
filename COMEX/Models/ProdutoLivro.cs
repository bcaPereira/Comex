namespace COMEX;

internal class ProdutoLivro : Produto
{
    public string Isbn { get; set; }
    public int TotalPaginas { get; set; }
    public override string Classificacao => "Livro";

    public ProdutoLivro(int id, string nome, decimal preco, string isbn, int totalPaginas, string descricao = "", int quantidade = 0)
        : base(id, nome, preco, descricao, quantidade)
    {
        Isbn = isbn;
        TotalPaginas = totalPaginas;
    }
}
