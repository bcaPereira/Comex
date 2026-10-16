class DadosIniciais
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
            new Produto("Notebook Pro 14", 4599.90m, "Notebook para trabalho", 18),
            new Produto("Mouse Sem Fio", 89.90m, "Mouse ergonômico", 65),
            new Produto("Teclado Mecânico", 279.00m, "Teclado ABNT2", 34),
            new Produto("Monitor 24\"", 799.90m, "Full HD", 22),
            new Produto("Headset Gamer", 349.50m, "Com microfone", 27),
            new Produto("Webcam HD", 199.90m, "1080p", 41),
            new Produto("SSD 1TB", 529.90m, "NVMe", 30),
            new Produto("Cadeira Office", 1299.00m, "Cadeira ergonômica", 12),
            new Produto("Mesa Digital", 999.00m, "Mesa para escritório", 9),
            new Produto("Hub USB-C", 149.90m, "7 portas", 38),
            new Produto("Carregador 65W", 189.90m, "USB-C", 50),
            new Produto("Suporte Monitor", 239.90m, "Articulado", 21),
            new Produto("Impressora Laser", 1399.00m, "Mono", 11),
            new Produto("Roteador Wi-Fi 6", 679.90m, "Dual Band", 17),
            new Produto("Smartphone X", 2899.00m, "128GB", 14),
            new Produto("Tablet Plus", 2199.00m, "10 polegadas", 16),
            new Produto("Power Bank", 179.90m, "20.000mAh", 44),
            new Produto("Cabo HDMI", 49.90m, "2 metros", 80),
            new Produto("Adaptador VGA", 39.90m, "HDMI para VGA", 36),
            new Produto("Caixa de Som", 259.00m, "Bluetooth", 24)
        ];
    }
}
