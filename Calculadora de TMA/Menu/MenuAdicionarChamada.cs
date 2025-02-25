using Calculadora_de_TMA.Banco;
using Calculadora_de_TMA.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Menu;

internal class MenuAdicionarChamada : Menu
{
    public override void Executar(CalculadoraDeTmaContext context)
    {
        base.Executar(context);
        MostrarMenu("Menu Adicionar Chamada");
        var assistenteDal = new DAL<Assistente>(context);
        var chamadaDal = new DAL<Chamada>(context);
        Console.Write("Digite o nome do assistente que atendeu a chamada: ");
        string nomeDoAssistente = Console.ReadLine()!;
        Assistente? assistenteEncontrado = assistenteDal.Buscar(a => a.Nome.Equals(nomeDoAssistente));
        if (assistenteEncontrado == null)
        {
            Console.WriteLine("Assistente não encontrado!");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }
        Console.Write("Digite o tempo de conversa da chamada: ");
        int tempoDeConversa = int.Parse(Console.ReadLine()!);
        Console.Write("Digite o tempo de espera da chamada: ");
        int tempoDeEspera = int.Parse(Console.ReadLine()!);
        Console.Write("Digite o nome da linha da chamada: ");
        string nomeDaLinha = Console.ReadLine()!;
        Linha? linhaEncontrada = context.Linhas.FirstOrDefault(l => l.Nome.Equals(nomeDaLinha));
        if (!context.LinhasAssistentes.Any(la => la.AssistenteId == assistenteEncontrado.AssistenteId && la.LinhaId == linhaEncontrada.LinhaId))
        {
            Console.WriteLine($"O assistente {assistenteEncontrado.Nome} não possui a linha {nomeDaLinha}");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }
        chamadaDal.Adicionar(new Chamada(assistenteEncontrado, tempoDeConversa, tempoDeEspera, linhaEncontrada));
        //assistenteEncontrado.MostrarLinhas();
        //assistenteDal.Atualizar(assistenteEncontrado);
        Console.WriteLine("Chamada adicionada com sucesso!");
        Thread.Sleep(4000);
    }
}
