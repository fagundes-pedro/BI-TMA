namespace Calculadora_de_TMA.Modelos;

public class Linha
{
    public Linha(string nome)
    {
        Nome = nome;
    }
    public int LinhaId { get; set; }
    public string Nome { get; set; }
    public virtual ICollection<Chamada> Chamadas { get; set; }
    public virtual ICollection<LinhaAssistente> LinhasAssistentes { get; set; }

}