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
        var linhaAssistenteDal = new DAL<LinhaAssistente>(context);
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
            assistente.CalcularTMA();
            foreach (var linhaAssistente in assistente.LinhasAssistentes)
            {
                linhaAssistenteDal.Atualizar(linhaAssistente);
            }
            assistente.MostrarLinhas();
        }
        Console.Write("\n\nPressione qualquer tecla para voltar ao menu principal.");
        Console.ReadKey();
    }
}
