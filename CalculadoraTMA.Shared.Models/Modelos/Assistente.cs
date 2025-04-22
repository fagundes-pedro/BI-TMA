using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BI_TMA.Shared.Models.Modelos;

public class Assistente
{
    public Assistente(string nome)
    {
        Nome = nome;
        Chamadas = new List<Chamada>();
        LinhasAssistentes = new List<LinhaAssistente>();
    }
    public int AssistenteId { get; set; }
    public string Nome { get; set; }
    public virtual ICollection<LinhaAssistente> LinhasAssistentes { get; set; }
    public virtual ICollection<Chamada> Chamadas { get; set; }


    public void MostrarLinhas()
    {
        Console.WriteLine($"\nLinhas do assistente {Nome}:");
        foreach (var linha in LinhasAssistentes)
        {
            if (linha.TMA != 0)
            {
                Console.WriteLine($"- {linha.Linha.Nome} - TMA {linha.TMA/1000} segundos");
            }
            else
            {
                Console.WriteLine($"- {linha.Linha.Nome} - TMA não disponível");
            }
        }
    }
    public void CalcularTMA()
    {
        foreach (var linha in LinhasAssistentes)
        {
            if (Chamadas.Count(c => c.Linha.Nome.Equals(linha.Linha.Nome)) == 0)
            {
                linha.TMA = 0;
            }
            else
            {
                linha.TMA = Chamadas.Where(c => c.Linha.Nome.Equals(linha.Linha.Nome)).Sum(c => c.TempoDeChamada) / Chamadas.Count(c => c.Linha.Nome.Equals(linha.Linha.Nome));
            }
        }
    }
}
