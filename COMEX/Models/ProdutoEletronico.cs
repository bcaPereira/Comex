namespace COMEX;

internal class ProdutoEletronico : Produto
{
    public int Voltagem { get; set; }
    public int Potencia { get; set; }
    public override string Classificacao => "Eletronico";

    public ProdutoEletronico(int id, string nome, decimal preco, int voltagem, int potencia, string descricao = "", int quantidade = 0)
        : base(id, nome, preco, descricao, quantidade)
    {
        Voltagem = voltagem;
        Potencia = potencia;
    }
}
