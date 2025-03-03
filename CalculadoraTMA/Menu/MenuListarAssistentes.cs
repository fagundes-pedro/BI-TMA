using Calculadora_de_TMA.Banco;
using Calculadora_de_TMA.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Menu;

internal class MenuListarAssistentes : Menu
{
    public override void Executar(CalculadoraDeTmaContext context)
    {
        base.Executar(context);
        MostrarMenu("Menu Listar Assistentes");
        var assistenteDal = new DAL<Assistente>(context);
        List<Assistente> assistentes = assistenteDal.Listar();
        if (assistentes.Count == 0)
        {
            Console.WriteLine("Nenhum assistente cadastrado!");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }
        foreach (Assistente assistente in assistentes)
        {
            assistente.MostrarLinhas();
        }
        Console.Write("\n\nPressione qualquer tecla para voltar ao menu principal.");
        Console.ReadKey();
    }
}
