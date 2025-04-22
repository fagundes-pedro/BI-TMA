namespace BI_TMA.API.Response;

public record AssistenteResponse(int Id, string Nome, List<LinhaResponse> Linhas);
