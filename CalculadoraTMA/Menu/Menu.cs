using BI_TMA.Shared.DB.Banco;
using BI_TMA.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Menu;

public class Menu
{
    public void MostrarMenu(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = new string('*', quantidadeDeLetras + 4);
        Console.WriteLine(asteriscos);
        Console.WriteLine($"* {titulo} *");
        Console.WriteLine(asteriscos + "\n");
    }

    public virtual void Executar(CalculadoraDeTmaContext context)
    {
        Console.Clear();
    }
}
