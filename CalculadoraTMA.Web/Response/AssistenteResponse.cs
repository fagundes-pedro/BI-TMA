namespace BI_TMA.Web.Response;

public record AssistenteResponse(int Id, string Nome, List<LinhaResponse> Linhas);
