using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Modelos;

public class Chamada
{
    public Chamada()
    {
        
    }
    public Chamada(Guid id, DateTime dataHora, Assistente assistente, int tempoDeCoversa, int tempoDeEspera, Linha linha)
    {
        ChamadaId = id;
        DataHora = dataHora;
        Assistente = assistente;
        TempoDeChamada = tempoDeCoversa + tempoDeEspera;
        Linha = linha;
    }
    public Guid ChamadaId { get; set; }
    public DateTime DataHora { get; set; }
    public int AssistenteId { get; set; }
    public virtual Assistente Assistente { get; set; }
    public int LinhaId { get; set; }
    public virtual Linha Linha { get; set; }
    public double TempoDeChamada { get; set; }

    public override string ToString()
    {
        return $"{Assistente.Nome}, {TempoDeChamada}, {Linha.Nome}";
    }
}
