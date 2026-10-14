using System.Reflection.Metadata.Ecma335;


Dictionary<string, Cliente> clientes = new Dictionary<string, Cliente>();

Dictionary<string, Produto> produtos = new Dictionary<string, Produto>();
Dictionary<string, Carrinho> carrinhos = new Dictionary<string, Carrinho>();

while (ExibirMenuDeOpcoes() == 1)
{

    
}



int ExibirMenuDeOpcoes()
{
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Cadastrar cliente");
    Console.WriteLine("2 - Listar clientes");
    Console.WriteLine("3 - Cadastrar produto");
    Console.WriteLine("4 - Ajustar preço de produto");
    Console.WriteLine("5 - Listar produtos");
    Console.WriteLine("6 - Adicionar produto no carrinho");
    Console.WriteLine("7 - Listar carrinho");
    Console.WriteLine("8 - Fechar Compra");
    Console.WriteLine("-1 - Sair");
    int i = int.Parse(Console.ReadLine());
    switch (i)
    {
        case 1:
            Console.Clear();
            Console.WriteLine("Cadastrar cliente");
            CadastrarCliente();
            Console.Clear();
            break;
        case 2:
            Console.Clear();
            Console.WriteLine("Listar Clientes");
            ListaClientes();
            Console.Clear();
            break;
        case 3:
            Console.Clear();
            Console.WriteLine("Cadastrar produto");
            CadastraProduto();
            Console.Clear();
            break;
        case 4:
            Console.Clear();
            Console.WriteLine("Ajustar preço de produto");
            AjustarPrecoDeProduto();
            Console.Clear();
            break;
        case 5:
            Console.Clear();
            Console.WriteLine("Listar produtos");
            ListaProdutos();
            Espera();
            Console.Clear();
            break;
        case 6:
            Console.Clear(); Console.WriteLine("Adicionar produto no carrinho");
            AdicionaProdutoNoCarrinho();
            Espera();
            Console.Clear();
            break;
        case 7:
            Console.Clear();
            Console.WriteLine("Listar carrinho");
            ListarCarrinho();
            Espera();
            Console.Clear();
            break;
        case 8:
            Console.Clear();
            Console.WriteLine("Fechar Compra");
            FechaCompra();
            Espera();
            Console.Clear();
            break;
        case -1:
            Console.WriteLine("COMEX FINALIZADO!");
            Espera();
            return 0;
            break;
        default:
            Console.WriteLine("Opção inválida");
            break;
    }
    return 1;
}

void CadastrarCliente()
{
    Console.WriteLine("Digite o nome do cliente:");
    string nome = Console.ReadLine();
    Console.WriteLine("Digite o CPF do cliente:");
    string cpf = Console.ReadLine();
    Cliente a = new()
    {
        Nome = nome,
        CPF = cpf
    };
    clientes.Add(cpf, a);
    Console.Clear();
    Console.WriteLine($"Usuário {a.Nome} ({a.CPF}) cadastrado com sucesso.");
    Espera();
}

void ListaClientes()
{
    foreach(var cliente in clientes.Values)
    {
        Console.WriteLine($"Nome: {cliente.Nome} - CPF: {cliente.CPF}");
    }
    Espera();
}

void CadastraProduto()
{
    Console.WriteLine("Digite o nome do produto:");
    string nome = Console.ReadLine();
    Console.WriteLine("Digite o preço do produto:");
    string preco = Console.ReadLine();
    Produto p = new()
    {
        Nome = nome,
        Preço = preco
    };
    produtos.Add(nome, p);
    Console.Clear();
    Console.WriteLine($"Produto {p.Nome} ({p.Preço}) cadastrado com sucesso.");
    Espera();
}

void AjustarPrecoDeProduto()
{
    Console.WriteLine("Digite o nome do produto:");
    string nome = Console.ReadLine();
    if (produtos.ContainsKey(nome))
    {
        Console.WriteLine("Digite o novo preço do produto:");
        string preco = Console.ReadLine();
        produtos[nome].Preço = preco;
        Console.WriteLine($"Preço do produto {nome} ajustado para {preco}.");
    }
    else
    {
        Console.WriteLine($"Produto {nome} não encontrado.");
    }
    Espera();
}
void ListaProdutos()
{
    foreach (var produto in produtos.Values)
    {
        Console.WriteLine($"Nome: {produto.Nome} - Preço: {produto.Preço}");
    }
    
}   

void AdicionaProdutoNoCarrinho()
{
    Console.WriteLine("Informe o CPF do cliente:"); 
    string cpf = Console.ReadLine();
    if (clientes.ContainsKey(cpf))
    {
        if (!carrinhos.ContainsKey(cpf))
        {
            carrinhos[cpf] = new Carrinho();
        }
        carrinhos[cpf].Cliente = clientes[cpf];
        Console.Clear();
        Console.WriteLine($">>> Cliente {carrinhos[cpf].Cliente.Nome} selecionado.");
        Console.WriteLine("############# LISTA DE PRODUTOS DISPONÍVEIS #############");
        ListaProdutos();
        Console.WriteLine("############# ----------------------------- #############");
        Console.WriteLine("Digite o nome do produto que deseja adicionar ao carrinho:");
        string nomeProduto = Console.ReadLine();
        
        if (produtos.ContainsKey(nomeProduto))
        {
            carrinhos[cpf].Produtos.Add(produtos[nomeProduto]);
            Console.WriteLine($"Produto {nomeProduto} adicionado ao carrinho do cliente {carrinhos[cpf].Cliente.Nome}.");
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
        
    }
    else
    {
        Console.WriteLine("Usuário não cadastrado.");
    }   
}

void ListarCarrinho()
{
    Console.WriteLine("Informe o CPF do cliente:");
    string cpf = Console.ReadLine();
    if (carrinhos.ContainsKey(cpf))
    {
        Console.Clear();
        var carrinho = carrinhos[cpf];
        Console.WriteLine($">>> Carrinho do cliente {carrinho.Cliente.Nome}:\n");
        foreach (var produto in carrinho.Produtos)
        {
            Console.Write($"Produto: {produto.Nome} - Preço: {produto.Preço} | ");
        }
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("Carrinho não encontrado para este cliente.");
    }
}

void FechaCompra()
{
    Console.WriteLine("Informe o CPF do cliente:");
    string cpf = Console.ReadLine();
    if (carrinhos.ContainsKey(cpf))
    {
        Console.WriteLine($">>> Carrinho do cliente {carrinhos[cpf].Cliente.Nome} selecionado \n");
        var carrinho = carrinhos[cpf];
        decimal total = 0;
        foreach (var produto in carrinho.Produtos)
        {
            Console.WriteLine($"Produto: {produto.Nome}...............R$ {produto.Preço},00");
            total += decimal.Parse(produto.Preço);
        }
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"Total: R${total}");
        Console.WriteLine("Deseja finalizar a compra? (S/N)");
        string resposta = Console.ReadLine();
        if (resposta.ToUpper() == "S")
        {
            Console.WriteLine("Insira os dados do cartão:");
            string dadosCartao = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"Realizando pagamento de R$ {total} no cartão {dadosCartao}.");
            Animacao();
            
            Console.WriteLine($"Pagamento de R$ {total} no cartão {dadosCartao} realizado com sucesso para {carrinhos[cpf].Cliente.Nome} ({carrinhos[cpf].Cliente.CPF}).");
            carrinhos.Remove(cpf);
        }
        else
        {
            Console.WriteLine("Compra cancelada.");
        }
         // Limpa o carrinho após a compra
    }
    else
    {
        Console.WriteLine("Cliente não cadastrado.");
    }
    Espera();
}

void Espera()
{
    Console.WriteLine("Aperte enter para continuar...");
    Console.ReadLine();
}

void Animacao()
{
    string mensagem = "P.R.O.C.E.S.S.A.N.D.O...C.O.M.P.R.A.";

    for (int repeticao = 0; repeticao < 3; repeticao++)
    {
        Console.Clear();

        foreach (char letra in mensagem)
        {
            Console.Write(letra);
            Thread.Sleep(5000 / mensagem.Length);
        }

        Thread.Sleep(5000 - (5000 / mensagem.Length) * mensagem.Length);
    }

    Console.Clear();
    Console.WriteLine("Compra processada!");
}


class Produto
{
    public string Nome { get; set; }
    public string Preço { get; set; }
}

class Cliente
{
    public string Nome { get; set; }
    public string CPF { get; set; }
}

class Carrinho
{
    public List<Produto> Produtos { get; set; } = new List<Produto>();
    public Cliente Cliente { get; set; }
}