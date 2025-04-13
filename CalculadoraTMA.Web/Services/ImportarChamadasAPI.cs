using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CalculadoraTMA.Web.Services;

public class ImportarChamadasAPI
{
    private readonly HttpClient _httpClient;

    public ImportarChamadasAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }


    public async Task<string> EnviarFicheiroAsync(Stream fileStream, string fileName)
    {
        if (fileStream == null || string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException("O arquivo e o nome do arquivo não podem ser nulos ou vazios.");
        }

        using var content = new MultipartFormDataContent();
        var fileContent = new StreamContent(fileStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        content.Add(fileContent, "file", fileName);

        var response = await _httpClient.PostAsync("/Chamadas/Upload", content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao enviar o ficheiro: {response.StatusCode} - {error}");
        }
    }
}
