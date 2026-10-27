using System.Globalization;

namespace COMEX;

internal partial class Program
{
    private static string LerTextoObrigatorio(string mensagem)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(entrada))
            {
                return entrada.Trim();
            }

            Console.WriteLine("Entrada inválida. O valor não pode ser vazio.");
        }
    }

    private static int LerInteiroValido(string mensagem, int valorMinimo)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();
            if (int.TryParse(entrada, out int valor) && valor >= valorMinimo)
            {
                return valor;
            }

            Console.WriteLine($"Entrada inválida. Digite um número inteiro maior ou igual a {valorMinimo}.");
        }
    }

    private static decimal LerDecimalValido(string mensagem, decimal valorMinimo)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();

            bool valorAtual = decimal.TryParse(
                entrada,
                NumberStyles.Number,
                CultureInfo.CurrentCulture,
                out decimal valor);

            bool valorInvariante = decimal.TryParse(
                entrada,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out decimal valorInvarianteConvertido);

            if (valorAtual && valor >= valorMinimo)
            {
                return valor;
            }

            if (valorInvariante && valorInvarianteConvertido >= valorMinimo)
            {
                return valorInvarianteConvertido;
            }

            Console.WriteLine($"Entrada inválida. Digite um número maior ou igual a {valorMinimo}.");
        }
    }

    private static string LerOpcaoSimOuNao(string mensagem)
    {
        while (true)
        {
            Console.WriteLine(mensagem);
            string? entrada = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(entrada))
            {
                string valor = entrada.Trim().ToUpperInvariant();
                if (valor == "S" || valor == "N")
                {
                    return valor;
                }
            }

            Console.WriteLine("Entrada inválida. Digite S para sim ou N para não.");
        }
    }

    private static string LimitarTexto(string texto, int tamanhoMaximo)
    {
        if (texto.Length <= tamanhoMaximo)
        {
            return texto;
        }

        return texto[..(tamanhoMaximo - 3)] + "...";
    }

    private static Produto? BuscarProdutoPorNomeOuId(string entrada)
    {
        string valor = entrada.Trim();

        bool todosNumericos = valor.Length > 0 && valor.All(char.IsDigit);
        if (todosNumericos && int.TryParse(valor, out int id))
        {
            return produtos.Values.FirstOrDefault(p => p.Id == id);
        }

        string nomeNormalizado = NormalizarNomeProduto(valor);
        return produtos.TryGetValue(nomeNormalizado, out Produto? produto) ? produto : null;
    }

    private static bool TentarInterpretarEntradaProduto(string entrada, out string seletorProduto, out int? quantidade)
    {
        seletorProduto = string.Empty;
        quantidade = null;

        string valor = entrada.Trim();
        if (string.IsNullOrWhiteSpace(valor))
        {
            return false;
        }

        int indiceX = valor.LastIndexOf('x');
        if (indiceX < 0)
        {
            indiceX = valor.LastIndexOf('X');
        }

        if (indiceX > 0)
        {
            string possivelProduto = valor[..indiceX].Trim();
            string possivelQuantidade = valor[(indiceX + 1)..].Trim();

            if (!string.IsNullOrWhiteSpace(possivelProduto) && int.TryParse(possivelQuantidade, out int qtd))
            {
                if (qtd <= 0)
                {
                    return false;
                }

                seletorProduto = possivelProduto;
                quantidade = qtd;
                return true;
            }
        }

        seletorProduto = valor;
        return true;
    }

    private static int LerQuantidadeProduto()
    {
        while (true)
        {
            Console.Write("Quantidade (Enter = 1): ");
            string? entrada = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entrada))
            {
                return 1;
            }

            if (int.TryParse(entrada.Trim(), out int quantidade) && quantidade > 0)
            {
                return quantidade;
            }

            Console.WriteLine("Quantidade inválida. Digite um número inteiro maior que zero.");
        }
    }

    private static string NormalizarNomeProduto(string nome)
    {
        string valor = nome.Trim();
        bool todosNumericos = valor.Length > 0 && valor.All(char.IsDigit);
        return todosNumericos ? $"'{valor}'" : valor;
    }

    private static void Animacao()
    {
        string[] etapas =
        {
            "Validando dados do pagamento",
            "Autorizando transação",
            "Finalizando compra"
        };

        char[] spinner = { '|', '/', '-', '\\' };
        int larguraBarra = 26;
        bool cursorVisivel = Console.CursorVisible;
        Console.CursorVisible = false;

        try
        {
            foreach (string etapa in etapas)
            {
                for (int progresso = 0; progresso <= 100; progresso += 4)
                {
                    int preenchido = progresso * larguraBarra / 100;
                    string barra = new string('█', preenchido) + new string('·', larguraBarra - preenchido);
                    char frame = spinner[(progresso / 4) % spinner.Length];

                    Console.Write($"\r{frame} {etapa} [{barra}] {progresso,3}%");
                    Thread.Sleep(55);
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\r✔ ");
                Console.ResetColor();
                Console.WriteLine($"{etapa} [concluído]");
            }

            Console.WriteLine();
            ExibirMensagem("Compra processada com sucesso!", ConsoleColor.Green);
        }
        finally
        {
            Console.CursorVisible = cursorVisivel;
        }
    }
}
