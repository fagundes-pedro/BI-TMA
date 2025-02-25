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
    public Chamada(Assistente assistente, int tempoDeCoversa, int tempoDeEspera, Linha linha)
    {
        Assistente = assistente;
        TempoDeChamada = tempoDeCoversa + tempoDeEspera;
        Linha = linha;
    }
    public int ChamadaId { get; set; }
    public int AssistenteId { get; set; }
    public virtual Assistente Assistente { get; set; }
    public int LinhaId { get; set; }
    public virtual Linha Linha { get; set; }
    public int TempoDeChamada { get; set; }

    public override string ToString()
    {
        return $"{Assistente.Nome}, {TempoDeChamada}, {Linha.Nome}";
    }
}
