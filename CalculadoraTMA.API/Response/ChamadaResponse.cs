namespace CalculadoraTMA.API.Response;

public record ChamadaResponse(string Id, string DataHora, string NomeAssistente, string NomeLinha, Double Duracao);