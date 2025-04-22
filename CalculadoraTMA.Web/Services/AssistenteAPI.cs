using CalculadoraTMA.Web.Request;
using CalculadoraTMA.Web.Response;
using System.Net.Http;
using System.Net.Http.Json;

namespace CalculadoraTMA.Web.Services;

public class AssistenteAPI
{
    private readonly HttpClient _httpClient;
    public AssistenteAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }
    public async Task<ICollection<AssistenteResponse>?> ListarAssistentesAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<AssistenteResponse>>("/Assistentes");
    }

    public async Task CriarAssistenteAsync(AssistenteRequest assistente)
    {
        var response = await _httpClient.PostAsJsonAsync("/Assistentes", assistente);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Erro ao criar assistente");
        }
    }
    public async Task EliminarAssistenteAsync(int Id)
    {
        var response = await _httpClient.DeleteAsync($"/Assistentes/{Id}");
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Erro ao eliminar assistente");
        }
    }
}
