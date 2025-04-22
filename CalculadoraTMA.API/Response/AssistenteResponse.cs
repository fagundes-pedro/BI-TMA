namespace CalculadoraTMA.API.Response;

public record AssistenteResponse(int Id, string Nome, List<LinhaResponse> Linhas);
