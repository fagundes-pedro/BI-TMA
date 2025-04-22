using BI_TMA.Shared.DB.Banco;
using BI_TMA.Shared.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Menu;

internal class MenuAdicionarLinha : Menu
{
    public override void Executar(CalculadoraDeTmaContext context)
    {
        base.Executar(context);
        var assistenteDal = new DAL<Assistente>(context);
        var linhaDal = new DAL<Linha>(context);
        var linhaAssistenteDal = new DAL<LinhaAssistente>(context);
        MostrarMenu("Menu Adicionar Linha");
        Console.Write("Digite o nome do assistente que deseja adicionar uma linha: ");
        string nomeDoAssistente = Console.ReadLine()!;
        Assistente? assistenteEncontrado = assistenteDal.Buscar(a => a.Nome.Equals(nomeDoAssistente));
        if (assistenteEncontrado == null)
        {
            Console.WriteLine("Assistente não encontrado!");
            Console.Write("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }
        Console.Write("Digite o nome da linha que deseja adicionar: ");
        string nomeDaLinha = Console.ReadLine()!;
        Linha? linhaEncontrada = linhaDal.Buscar(l => l.Nome.Equals(nomeDaLinha));
        if (linhaEncontrada == null)
        {
            Console.WriteLine("A Linha digitada não existe!");
            Console.Write("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }
        if (context.LinhasAssistentes.Any(la => la.AssistenteId == assistenteEncontrado.AssistenteId && la.LinhaId == linhaEncontrada.LinhaId))
        {
            Console.WriteLine("O assistente já tem a Linha digitada!");
            Console.Write("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }
        linhaAssistenteDal.Adicionar(new LinhaAssistente(assistenteEncontrado.AssistenteId, linhaEncontrada.LinhaId));
        Console.WriteLine($"Linha {nomeDaLinha} adicionada ao assistente {nomeDoAssistente} com sucesso!");
        Thread.Sleep(4000);
    }
}
