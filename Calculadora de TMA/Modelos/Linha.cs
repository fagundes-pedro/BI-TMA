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

    //public void AdicionarChamadas(List<Chamada> chamadas)
    //{
    //    var chamadasDoAssistente = chamadas.Where(c => c.NomeDoAssistente.Equals(Assistente.Nome));
    //    var chamadasDaLinha = chamadasDoAssistente.Where(c => c.NomeDaLinha.Equals(Nome));
    //    foreach (var chamada in chamadasDaLinha)
    //    {
    //        Chamadas.Add(chamada);
    //        //Console.WriteLine($"Adicionada a chamada de {chamada.NomeDoAssistente}: {chamada.NomeDaLinha}, {chamada.TempoDeChamada}s");
    //    }
    //}
}