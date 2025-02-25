using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Modelos;

public class Assistente
{
    public Assistente(string nome)
    {
        Nome = nome;
    }
    public int AssistenteId { get; set; }
    public string Nome { get; set; }
    public virtual ICollection<LinhaAssistente> LinhasAssistentes { get; set; }
    public virtual ICollection<Chamada> Chamadas { get; set; }


    public void MostrarLinhas()
    {
        Console.WriteLine($"\nLinhas do assistente {Nome}:");
        //CalcularTMA();
        foreach (var linha in LinhasAssistentes)
        {
            int tma = CalcularTMA(linha.Linha);
            if (tma != 0)
            {
                Console.WriteLine($"- {linha.Linha.Nome} - TMA {tma} segundos");
            }
            else
            {
                Console.WriteLine($"- {linha.Linha.Nome} - TMA não disponível");
            }
        }
    }
    public int CalcularTMA(Linha linha)
    {
        if (Chamadas.Count(c => c.Linha.Nome.Equals(linha.Nome)) == 0)
        {
            return 0;
        }
        return Chamadas.Where(c => c.Linha.Nome.Equals(linha.Nome)).Sum(c => c.TempoDeChamada) / Chamadas.Count(c => c.Linha.Nome.Equals(linha.Nome));
    }
}
