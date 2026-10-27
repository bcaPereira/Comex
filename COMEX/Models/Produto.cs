namespace COMEX;

internal interface IProdutoClassificavel
{
    string Classificacao { get; }
}

internal abstract class Produto : IProdutoClassificavel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
    public decimal Preço { get; set; }
    public abstract string Classificacao { get; }

    protected Produto(int id, string nome, decimal preco, string descricao = "", int quantidade = 0)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Quantidade = quantidade;
        Preço = preco;
    }
}
