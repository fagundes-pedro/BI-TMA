using Calculadora_de_TMA.Banco;
using Calculadora_de_TMA.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Menu;

internal class Menu
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
