using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Modelos;

public class LinhaAssistente
{
    public LinhaAssistente(int assistenteId, int linhaId)
    {
        AssistenteId = assistenteId;
        LinhaId = linhaId;
    }
    public int AssistenteId { get; set; }
    public virtual Assistente Assistente { get; set; }
    public int LinhaId { get; set; }
    public virtual Linha Linha { get; set; }
}
