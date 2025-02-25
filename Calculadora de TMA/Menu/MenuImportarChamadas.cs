using Calculadora_de_TMA.Banco;
using Calculadora_de_TMA.Modelos;

namespace Calculadora_de_TMA.Menu;

internal class MenuImportarChamadas
{
    public void Executar(CalculadoraDeTmaContext context)
    {        
        Console.Clear();
        MostrarMenu("Menu Importar Chamadas");
        ImportarChamadas(context);
        Console.WriteLine("Importando chamadas...");
        Thread.Sleep(2000);
        Console.WriteLine("Chamadas importadas com sucesso!");
        Thread.Sleep(2000);
    }

    private void ImportarChamadas(CalculadoraDeTmaContext context)
    {
        DAL<Chamada> chamadaDal = new(context);
        DAL<Assistente> assistenteDal = new(context);
        List<Chamada> chamadas = ConversorDeCsvParaChamada.Converter("chamadas.csv");
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
