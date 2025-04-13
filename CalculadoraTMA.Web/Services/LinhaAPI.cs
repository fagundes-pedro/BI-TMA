using CalculadoraTMA.Web.Response;
using System.Net.Http.Json;

namespace CalculadoraTMA.Web.Services;

public class LinhaAPI
{
    private readonly HttpClient _httpClient;
    public LinhaAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }
    public async Task<ICollection<LinhaResponse>?> ListarLinhasAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<LinhaResponse>>("/Linhas");
    }
}
