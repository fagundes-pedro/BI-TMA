using BI_TMA.Shared.DB.Banco;
using BI_TMA.Shared.Models.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Menu;

internal class MenuAdicionarAssistente : Menu
{
    public override void Executar(CalculadoraDeTmaContext context)
    {
        base.Executar(context);
        var assistenteDal = new DAL<Assistente>(context);
        var linhaDal = new DAL<Linha>(context);
        var linhaAssistenteDal = new DAL<LinhaAssistente>(context);
        MostrarMenu("Menu Criar Assistente");
        Console.Write("Digite o nome do assitente que deseja adicionar: ");
        string nome = Console.ReadLine()!;
        Console.Write("Digite as linhas que o assistente atende separadas por virgulas: ");
        string[] linhas = Console.ReadLine()!.Split(',');
        Assistente assistente = new(nome);
        if (assistenteDal.Buscar(a => a.Nome.Equals(nome)) != null && assistenteDal.Buscar(a => a.Nome.Equals(nome)).Nome == nome)
        {
            Console.WriteLine("Assistente já cadastrado!");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            Console.ReadKey();
            return;
        }
        assistenteDal.Adicionar(assistente);
        Console.WriteLine("Assistente adicionado com sucesso!\n\n");
        foreach (var linha in linhas)
        {
            if (linhaDal.Buscar(l => l.Nome.Equals(linha)) == null)
            {
                Console.WriteLine($"Linha {linha} não encontrada!");
                Console.Write("Pressione qualquer tecla para voltar ao menu principal.");
                Console.ReadKey();
                return;
            }
            LinhaAssistente linhaAssistente = new(assistente.AssistenteId, linhaDal.Buscar(l => l.Nome.Equals(linha)).LinhaId);
            linhaAssistenteDal.Adicionar(linhaAssistente);
            Console.WriteLine($"Linha {linha} adicionada ao assistente {nome} com sucesso!\n");
        }
        Thread.Sleep(4000);
    }
}
