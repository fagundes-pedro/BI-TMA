using CalculadoraTMA.API.Response;
using System.Net.Http;
using System.Net.Http.Json;

namespace CalculadoraTMA.Web.Services;

public class ChamadaAPI
{
    private readonly HttpClient _httpClient;
    public ChamadaAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }
    public async Task<ICollection<ChamadaResponse>?> ListarAssistentesAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<ChamadaResponse>>("/Chamada");
    }
}
