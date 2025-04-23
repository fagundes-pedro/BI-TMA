using Calculadora_de_TMA;
using BI_TMA.Shared.Models.Modelos;
using BI_TMA.Shared.DB.Banco;


namespace Calculadora_de_TMA.Menu;

public class MenuImportarChamadas : Menu
{
    public void Executar(BI_TMAContext context)
    {
        base.Executar(context);
        MostrarMenu("Menu Importar Chamadas");
        ImportarChamadas(context);
        Console.WriteLine("Importando chamadas...");
        Thread.Sleep(2000);
        Console.WriteLine("Chamadas importadas com sucesso!");
        Thread.Sleep(2000);
    }

    private void ImportarChamadas(BI_TMAContext context)
    {
        DAL<Chamada> chamadaDal = new(context);
        DAL<Assistente> assistenteDal = new(context);
        List<Chamada> chamadas = ConversorDeCsvParaChamada.Converter("chamadas.csv", context);
        foreach (var chamada in chamadas)
        {
            chamadaDal.Adicionar(chamada);
        }
    }

    private void MostrarMenu(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = new string('*', quantidadeDeLetras + 4);
        Console.WriteLine(asteriscos);
        Console.WriteLine($"* {titulo} *");
        Console.WriteLine(asteriscos + "\n");
    }
}
