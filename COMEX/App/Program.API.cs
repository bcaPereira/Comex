using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace COMEX;

internal partial class Program
{
    private static async Task CarregarDadosAsync(string url = "https://fakestoreapi.com/products")
    {
        using HttpClient client = new HttpClient();

        try
        {
  

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            Console.WriteLine(json);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro HTTP: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine($"Timeout ou requisição cancelada: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
        
    }
}