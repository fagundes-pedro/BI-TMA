using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Modelos;

internal class ConversorDeCsvParaChamada
{
    public static List<Chamada> Converter(string caminho)
    {
        List<Chamada> chamadas = new List<Chamada>();
        string[] linhas = System.IO.File.ReadAllLines(caminho);
        foreach (var linha in linhas)
        {
            var campos = linha.Split(',');
            var nomeDoAssistente = campos[0];
            var tempoDeCoversa = int.Parse(campos[1]);
            var tempoDeEspera = int.Parse(campos[2]);
            var nomeDaLinha = campos[3];
            //chamadas.Add(new Chamada(nomeDoAssistente, tempoDeCoversa, tempoDeEspera, nomeDaLinha));
        }
        return chamadas;
    }
}
