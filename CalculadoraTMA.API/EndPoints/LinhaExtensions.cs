using BI_TMA.API.Request;
using BI_TMA.Shared.DB.Banco;
using BI_TMA.Shared.Models.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace BI_TMA.API.EndPoints;

public static class LinhaExtensions
{
    public static void AddEndPointLinhas(this WebApplication app)
    {
        app.MapGet("/Linhas", async ([FromServices] DAL<Linha> linhaDal) =>
        {
            var linhas = await linhaDal.ListarAsync();
            if (linhas is null || !linhas.Any())
            {
                return Results.NotFound();
            }
            var resultado = linhas.Select(linha => new
            {
                linha.Nome,
                Assistentes = linha.LinhasAssistentes.Select(assistente => new
                {
                    assistente.Assistente.Nome,
                    TMA = $"{Math.Round(assistente.TMA / 1000, 0)} segundos"
                })
            });
            return Results.Ok(resultado);
        }).WithTags("Linhas");

        app.MapPut("/Linhas/{nome}", async (string nome, [FromServices] DAL<Linha> linhaDal, [FromServices] DAL<Assistente> assistenteDal, [FromServices] DAL<LinhaAssistente> linhaAssistenteDal, [FromBody] LinhaRequest linhaRequest) =>
        {
            var assistente = await assistenteDal.BuscarAsync(a => a.Nome.ToUpper().Equals(nome));
            var linha = await linhaDal.BuscarAsync(l => l.Nome.ToUpper().Equals(linhaRequest.nome.ToUpper()));
            if (assistente is null)
            {
                return Results.NotFound("Não foi encontrado assistente com o nome indicado.");
            }
            if (linha is null)
            {
                return Results.NotFound("Não foi encontrada linha com o nome indicado.");
            }
            var linhaAAdicionar = await linhaDal.BuscarAsync(l => l.Nome.Equals(linha.Nome));
            if (linhaAAdicionar is null)
            {
                return Results.NotFound("Não foi encontrada linha com o nome indicado.");
            }

            if (assistente.LinhasAssistentes.Any(l => l.Linha.Nome.Equals(linha.Nome)))
            {
                return Results.BadRequest("Linha já está associada ao assistente.");
            }

            await linhaAssistenteDal.AdicionarAsync(new LinhaAssistente(assistente.AssistenteId, linhaAAdicionar.LinhaId));
            assistente.CalcularTMA();
            return Results.Ok($"Linha {linha.Nome} adicionada ao assistente {assistente.Nome} com sucesso");
        })
        .WithTags("Linhas");

        app.MapDelete("/Linhas/{nome}", async (string nome, [FromServices] DAL<LinhaAssistente> linhaAssistenteDal, [FromServices] DAL<Assistente> assistenteDal, [FromBody] string nomeDaLinha) =>
        {
            var assistente = await assistenteDal.BuscarAsync(a => a.Nome.ToUpper().Equals(nome));
            if (assistente is null)
            {
                return Results.NotFound("Não foi encontrado assistente com o nome indicado.");
            }
            var linhaAssistente = await linhaAssistenteDal.BuscarAsync(l => l.AssistenteId.Equals(assistente.AssistenteId) && l.Linha.Nome.ToUpper().Equals(nomeDaLinha.ToUpper()));
            if (linhaAssistente is null)
            {
                return Results.NotFound("Não foi encontrada linha com o nome indicado.");
            }
            await linhaAssistenteDal.RemoverAsync(linhaAssistente);
            return Results.NoContent();
        })
        .WithTags("Linhas");
    }
}
