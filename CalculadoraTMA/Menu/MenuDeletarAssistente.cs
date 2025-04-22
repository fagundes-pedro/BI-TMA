using BI_TMA.Shared.DB.Banco;
using BI_TMA.Shared.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Menu;

internal class MenuDeletarAssistente : Menu
{
    public override void Executar(CalculadoraDeTmaContext context)
    {
        base.Executar(context);
        var assistenteDal = new DAL<Assistente>(context);
        MostrarMenu("Menu Deletar Assistente");
        Console.Write("Digite o nome do assistente que deseja deletar: ");
        string nome = Console.ReadLine()!;
        Assistente? assistente = assistenteDal.Buscar(a => a.Nome.Equals(nome));
        if (assistente == null)
        {
            Console.WriteLine("Assistente não encontrado!");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }
        assistenteDal.Remover(assistente);
        Console.WriteLine("Assistente deletado com sucesso!");
        Thread.Sleep(4000);
    }
}
