using BI_TMA.Shared.DB.Banco;
using BI_TMA.Shared.Models.Modelos;
using BI_TMA.API.Response;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;

namespace BI_TMA.API.EndPoints;

public static class ChamadaExtensions
{
    public static void AddEndPointChamadas(this WebApplication app)
    {
        app.MapGet("/Chamadas", async ([FromServices] DAL<Chamada> chamadaDal) =>
        {
            var chamadas = await chamadaDal.ListarAsync();
            if (chamadas is null || !chamadas.Any())
            {
                return Results.NotFound("Não foram encontradas chamadas registadas");
            }
            var resultado = chamadas.Select(chamada =>
            {
                return new ChamadaResponse(
                    chamada.ChamadaId.ToString(),
                    chamada.DataHora.ToString("dd/MM/yyyy HH:mm:ss"),
                    chamada.Assistente.Nome,
                    chamada.Linha.Nome,
                    Math.Round(chamada.TempoDeChamada / 1000, 0)
                );
            });
            return Results.Ok(resultado);
        })
        .WithTags("Chamadas");

        /*app.MapGet("/Chamadas/{nome}", async (string nome, [FromServices] DAL<Chamada> chamadaDal) =>
        {
            var chamadas = await chamadaDal.ListarAsync();
            if (chamadas is null || !chamadas.Any())
            {
                return Results.NotFound($"Não foram encontradas chamadas registadas para o assistente {nome}");
            }
            var chamadasDoAssistente = chamadas.Where(chamada => chamada.Assistente.Nome.ToUpper().Equals(nome.ToUpper()));
            var resultado = chamadasDoAssistente.Select(chamada => new
            {
                chamada.ChamadaId,
                DataHora = chamada.DataHora.ToString("dd/MM/yyyy HH:mm:ss"),
                AssistenteNome = chamada.Assistente.Nome,
                LinhaNome = chamada.Linha.Nome,
                TempoDeChamda = $"{Math.Round(chamada.TempoDeChamada / 1000, 0)} segundos"
            });
            return Results.Ok(resultado);
        })
        .WithTags("Chamadas");*/

        app.MapPost("/Chamadas/Upload", async (HttpRequest request, [FromServices] DAL<Chamada> chamadaDal, [FromServices] DAL<Assistente> assistenteDal, [FromServices] DAL<Linha> linhaDal, [FromServices] DAL<LinhaAssistente> linhaAssistenteDal) =>
        {
            if (!request.HasFormContentType || !request.Form.Files.Any())
            {
                return Results.BadRequest("Nenhum arquivo foi enviado.");
            }

            var file = request.Form.Files[0];
            using var stream = new StreamReader(file.OpenReadStream());
            using var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null, // Ignora campos ausentes
                HeaderValidated = null, // Ignora validação de cabeçalhos
                BadDataFound = null // Ignora dados ruins
            });

            var chamadas = new List<Chamada>();
            var linhasAssistentes = await linhaAssistenteDal.ListarAsync();
            var assistentes = await assistenteDal.ListarAsync();
            var linhas = await linhaDal.ListarAsync();

            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var chamadaId = csv.GetField<string>("ID de conversa");
                var dataHora = csv.GetField<string>("Data");
                var assistenteNome = csv.GetField<string>("Usuários – Interagiram");
                var linhaNome = csv.GetField<string>("Fila");
                var tempoDeConversa = csv.GetField<string>("Total de conversas");
                var tempoDeEspera = csv.GetField<string>("Total em espera");

                if (!Guid.TryParse(chamadaId, out Guid chamadaIdGuid))
                {
                    chamadaIdGuid = new Guid();
                }

                if (!DateTime.TryParse(dataHora, out DateTime dataHoraParsed))
                {
                    dataHoraParsed = DateTime.MinValue;
                }

                if (!int.TryParse(tempoDeConversa, out int tempoDeConversaInt))
                {
                    tempoDeConversaInt = 0;
                }

                if (!int.TryParse(tempoDeEspera, out int tempoDeEsperaInt))
                {
                    tempoDeEsperaInt = 0;
                }

                if (string.IsNullOrEmpty(assistenteNome) || string.IsNullOrEmpty(linhaNome) || assistenteNome.Contains(';') || linhaNome.Contains(';'))
                {
                    continue;
                }

                var chamadaExistente = await chamadaDal.BuscarAsync(c => c.ChamadaId == chamadaIdGuid);
                if (chamadaExistente is not null)
                {
                    continue;
                }

                var assistente = assistentes.FirstOrDefault(a => a.Nome == assistenteNome);
                if (assistente is null)
                {
                    assistente = new Assistente(assistenteNome);
                    await assistenteDal.AdicionarAsync(assistente);
                    assistentes.Add(assistente);
                }

                var linha = linhas.FirstOrDefault(l => l.Nome == linhaNome);
                if (linha is null)
                {
                    linha = new Linha(linhaNome);
                    await linhaDal.AdicionarAsync(linha);
                    linhas.Add(linha);
                }


                var chamada = new Chamada(chamadaIdGuid, dataHoraParsed, assistente, tempoDeConversaInt, tempoDeEsperaInt, linha);
                chamadas.Add(chamada);
            }

            foreach (var chamada in chamadas)
            {
                var linhaAssistente = linhasAssistentes.FirstOrDefault(la => la.AssistenteId == chamada.AssistenteId && la.LinhaId == chamada.LinhaId);
                if (linhaAssistente is null)
                {
                    linhaAssistente = new LinhaAssistente(chamada.Assistente.AssistenteId, chamada.Linha.LinhaId);
                    if (!linhaAssistenteDal.context.ChangeTracker.Entries<LinhaAssistente>().Any(e => e.Entity.AssistenteId == linhaAssistente.AssistenteId && e.Entity.LinhaId == linhaAssistente.LinhaId))
                    {
                        await linhaAssistenteDal.AdicionarAsync(linhaAssistente);
                    }
                }
                chamada.Assistente.CalcularTMA();
                assistenteDal.Atualizar(chamada.Assistente);
                await chamadaDal.AdicionarAsync(chamada);
            }

            return Results.Ok("Chamadas registradas com sucesso.");
        })
        .WithTags("Chamadas");

        app.MapDelete("/Chamadas/All", async ([FromServices] DAL<Chamada> chamadaDal) =>
        {
            var chamadas = await chamadaDal.ListarAsync();
            if (chamadas is null || !chamadas.Any())
            {
                return Results.NotFound("Nenhuma chamada encontrada.");
            }
            foreach (var chamada in chamadas)
            {
                await chamadaDal.RemoverAsync(chamada);
            }
            return Results.Ok("Todas as chamadas foram deletadas com sucesso.");
        })
        .WithTags("Chamadas");
    }
}
