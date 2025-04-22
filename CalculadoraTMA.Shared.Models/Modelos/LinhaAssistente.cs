using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BI_TMA.Shared.Models.Modelos;

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
    public double TMA { get; set; }
}
