using Calculadora_de_TMA.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Modelos;

public class ConversorDeCsvParaChamada
{
    public static List<Chamada> Converter(string caminho, CalculadoraDeTmaContext context)
    {
        DAL<Assistente> assistenteDal = new(context);
        DAL<Linha> linhaDal = new(context);
        DAL<LinhaAssistente> linhaAssistenteDal = new(context);
        DAL<Chamada> chamadaDal = new(context);
        List<Chamada> chamadas = new List<Chamada>();
        string[] linhas = System.IO.File.ReadAllLines(caminho);
        foreach (var linha in linhas)
        {
            var campos = linha.Split(',');
            if (campos.Length != 5)
            {
                Console.WriteLine("A linha foi pulada\n");
                continue; // Skip lines with insufficient data
            }
            var chamadaId = Guid.Parse(campos[0].Trim('"'));
            var nomeDoAssistente = campos[1].Trim('"');
            var tempoDeCoversa = string.IsNullOrWhiteSpace(campos[2].Trim('"')) ? 0 : int.Parse(campos[2].Trim('"'));
            var tempoDeEspera = string.IsNullOrWhiteSpace(campos[3].Trim('"')) ? 0 : int.Parse(campos[3].Trim('"'));
            var nomeDaLinha = campos[4].Trim('"');
            
            Assistente assistente = new(nomeDoAssistente);
            if(assistenteDal.Buscar(a => a.Nome.Equals(nomeDoAssistente)) == null)
            {
                assistenteDal.Adicionar(assistente);
            }
            else
            {
                assistente = assistenteDal.Buscar(a => a.Nome.Equals(nomeDoAssistente));
            }
            if(linhaDal.Buscar(l => l.Nome.Equals(nomeDaLinha)) == null)
            {
                Linha linhaDaChamada = new(nomeDaLinha);
                linhaDal.Adicionar(linhaDaChamada);
            }
            if(linhaAssistenteDal.Buscar(la => la.Linha.Nome.Equals(nomeDaLinha) && la.Assistente.Nome.Equals(nomeDoAssistente)) == null)
            {
                LinhaAssistente linhaAssistente = new(assistenteDal.Buscar(a => a.Nome.Equals(nomeDoAssistente)).AssistenteId, linhaDal.Buscar(l => l.Nome.Equals(nomeDaLinha)).LinhaId);
                linhaAssistenteDal.Adicionar(linhaAssistente);
            }
            if (chamadaDal.Buscar(c => c.ChamadaId.Equals(chamadaId)) != null)
            {
                Console.WriteLine("Chamada já adicionada\n");
                continue;
            }
            chamadas.Add(new Chamada(chamadaId, DateTime.Now, assistente, tempoDeCoversa, tempoDeEspera, linhaDal.Buscar(l => l.Nome.Equals(nomeDaLinha))));
            Console.WriteLine("Chamada adicionada\n");
        }
        return chamadas;
    }
}
