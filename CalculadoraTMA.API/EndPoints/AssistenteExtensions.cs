using Calculadora_de_TMA.Banco;
using Calculadora_de_TMA.Modelos;
using CalculadoraTMA.API.Request;
using CalculadoraTMA.API.Response;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraTMA.API.EndPoints;

public static class AssistenteExtensions
{
    public static void AddEndPointAssistentes( this WebApplication app)
    {
        app.MapGet("/Assistentes", async ([FromServices] DAL<Assistente> assistenteDal) =>
        {
            var assistentes = await assistenteDal.ListarAsync();
            if (assistentes is null || !assistentes.Any())
            {
                return Results.NotFound();
            }
            var resultado = assistentes.Select(assistente =>
            {
                assistente.CalcularTMA();
                return new AssistenteResponse(
                    assistente.AssistenteId,
                    assistente.Nome,
                    assistente.LinhasAssistentes.Select(linha => new LinhaResponse(
                        linha.Linha.Nome,
                        $"{Math.Round(linha.TMA / 1000, 0)} segundos",
                        $"{assistente.Chamadas.Count(c => c.Linha.Nome.Equals(linha.Linha.Nome))} chamadas"
                    )).ToList()
                );
            });
            return Results.Ok(resultado);
        }).WithTags("Assistentes");

        app.MapGet("/Assistentes/{nome}", async (string nome, [FromServices] DAL<Assistente> assistenteDal) =>
        {
            var assistente = await assistenteDal.BuscarAsync(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (assistente is null)
            {
                return Results.NotFound("Não foi encontrado assistente com o nome indicado.");
            }
            assistente.CalcularTMA();
            var resultado = new
            {
                assistente.Nome,
                Linhas = assistente.LinhasAssistentes.Select(linha => new
                {
                    linha.Linha.Nome,
                    TMA = $"{Math.Round(linha.TMA / 1000, 0)} segundos",
                    NumeroDeChamadas = $"{assistente.Chamadas.Count(c => c.Linha.Nome.Equals(linha.Linha.Nome))} chamadas"
                })
            };
            return Results.Ok(resultado);
        }).WithTags("Assistentes");

        app.MapPost("/Assistentes", async ([FromServices] DAL<Assistente> assistenteDal, [FromBody] AssistenteRequest assistenteRequest) =>
        {
            var assistente = new Assistente(assistenteRequest.nome);
            await assistenteDal.AdicionarAsync(assistente);

            return Results.Ok($"Assistente {assistente.Nome} adicionado com sucesso");
        }).WithTags("Assistentes");

        app.MapPut("/Assistentes/{nome}", async (string nome, [FromServices] DAL<Assistente> assistenteDal, [FromBody] AssistenteRequest assistenteRequest) =>
        {
            var assistenteAModificar = await assistenteDal.BuscarAsync(a => a.Nome.ToUpper().Equals(nome));
            var assistente = new Assistente(assistenteRequest.nome);
            if (assistenteAModificar is null)
            {
                return Results.NotFound("Não foi encontrado assistente com o nome indicado.");
            }
            if (assistente.Nome is not null)
            {
                assistenteAModificar.Nome = assistente.Nome;
            }

            await assistenteDal.AtualizarAsync(assistenteAModificar);

            return Results.Ok($"Assistente {assistente.Nome} foi atualizado com sucesso");
        }).WithTags("Assistentes");

        app.MapDelete("/Assistentes/{id}", async ([FromServices] DAL<Assistente> assistenteDal, int id) =>

        {
            var assistente = await assistenteDal.BuscarAsync(a => a.AssistenteId.Equals(id));
            if (assistente is null)
            {
                return Results.NotFound("Não foi encontrado assistente com o Id indicado.");
            }
            await assistenteDal.RemoverAsync(assistente);
            return Results.NoContent();
        })
        .WithTags("Assistentes");
    }
}
