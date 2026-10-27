namespace COMEX;

internal class DadosIniciais
{
    public static List<Cliente> ObterClientes()
    {
        return
        [

             new Cliente { Nome = "Teste", CPF = "123", Email = "teste@comex.com", Telefone = "119999999", Profissao = "Compradora" },
            new Cliente { Nome = "Ana Souza", CPF = "11111111111", Email = "ana@comex.com", Telefone = "11990000001", Profissao = "Compradora" },
            new Cliente { Nome = "Bruno Lima", CPF = "22222222222", Email = "bruno@comex.com", Telefone = "11990000002", Profissao = "Gerente" },
            new Cliente { Nome = "Carla Mendes", CPF = "33333333333", Email = "carla@comex.com", Telefone = "11990000003", Profissao = "Analista" },
            new Cliente { Nome = "Diego Rocha", CPF = "44444444444", Email = "diego@comex.com", Telefone = "11990000004", Profissao = "Vendedor" },
            new Cliente { Nome = "Elisa Prado", CPF = "55555555555", Email = "elisa@comex.com", Telefone = "11990000005", Profissao = "Designer" },
            new Cliente { Nome = "Felipe Castro", CPF = "66666666666", Email = "felipe@comex.com", Telefone = "11990000006", Profissao = "Engenheiro" },
            new Cliente { Nome = "Gabriela Nunes", CPF = "77777777777", Email = "gabriela@comex.com", Telefone = "11990000007", Profissao = "Coordenadora" },
            new Cliente { Nome = "Henrique Dias", CPF = "88888888888", Email = "henrique@comex.com", Telefone = "11990000008", Profissao = "Supervisor" },
            new Cliente { Nome = "Isabela Gomes", CPF = "99999999999", Email = "isabela@comex.com", Telefone = "11990000009", Profissao = "Consultora" },
            new Cliente { Nome = "Julio Ferreira", CPF = "00000000000", Email = "joao@comex.com", Telefone = "11990000010", Profissao = "Empresário" }
        ];
    }

    public static List<Produto> ObterProdutos()
    {
        return
        [
            new ProdutoEletronico(1, "Notebook Pro 14", 4599.90m, 220, 65, "Notebook para trabalho", 18),
            new ProdutoEletronico(2, "Mouse Sem Fio", 89.90m, 5, 3, "Mouse ergonômico", 65),
            new ProdutoEletronico(3, "Teclado Mecânico", 279.00m, 5, 5, "Teclado ABNT2", 34),
            new ProdutoEletronico(4, "Monitor 24\"", 799.90m, 220, 45, "Full HD", 22),
            new ProdutoEletronico(5, "Headset Gamer", 349.50m, 5, 7, "Com microfone", 27),
            new ProdutoEletronico(6, "Webcam HD", 199.90m, 5, 4, "1080p", 41),
            new ProdutoEletronico(7, "SSD 1TB", 529.90m, 5, 5, "NVMe", 30),
            new ProdutoOutro(8, "Cadeira Office", 1299.00m, "Cadeira ergonômica", 12),
            new ProdutoOutro(9, "Mesa Digital", 999.00m, "Mesa para escritório", 9),
            new ProdutoEletronico(10, "Hub USB-C", 149.90m, 5, 10, "7 portas", 38),
            new ProdutoEletronico(11, "Carregador 65W", 189.90m, 220, 65, "USB-C", 50),
            new ProdutoOutro(12, "Suporte Monitor", 239.90m, "Articulado", 21),
            new ProdutoEletronico(13, "Impressora Laser", 1399.00m, 220, 350, "Mono", 11),
            new ProdutoEletronico(14, "Roteador Wi-Fi 6", 679.90m, 12, 18, "Dual Band", 17),
            new ProdutoEletronico(15, "Smartphone X", 2899.00m, 5, 30, "128GB", 14),
            new ProdutoEletronico(16, "Tablet Plus", 2199.00m, 5, 20, "10 polegadas", 16),
            new ProdutoEletronico(17, "Power Bank", 179.90m, 5, 20, "20.000mAh", 44),
            new ProdutoOutro(18, "Cabo HDMI", 49.90m, "2 metros", 80),
            new ProdutoOutro(19, "Adaptador VGA", 39.90m, "HDMI para VGA", 36),
            new ProdutoEletronico(20, "Caixa de Som", 259.00m, 5, 20, "Bluetooth", 24),
            new ProdutoLivro(21, "Clean Code", 129.90m, "9780132350884", 464, "Boas práticas de código", 15),
            new ProdutoLivro(22, "Domain-Driven Design", 189.90m, "9780321125217", 560, "Modelagem de domínio", 8),
            new ProdutoLivro(23, "Refactoring", 179.90m, "9780201485677", 448, "Melhoria de código legado", 10),
            new ProdutoLivro(24, "The Pragmatic Programmer", 149.90m, "9780135957059", 352, "Práticas de engenharia de software", 12)
        ];
    }
}
