using BI_TMA.Shared.DB;
using Calculadora_de_TMA.Menu;
using BI_TMA.Shared.Models;
using BI_TMA.Shared.DB.Banco;

var context = new BI_TMAContext();

var menu = new Menu();
string opcaoDigitada = "";
int opcao = -1;

while (opcao != 9)
{
    menu.Executar(context);
    menu.MostrarMenu("Menu Principal");

    Console.WriteLine("1 - Cadastrar assistente");
    Console.WriteLine("2 - Listar assistentes");
    Console.WriteLine("3 - Deletar assistente");
    Console.WriteLine("4 - Adicionar chamada");
    Console.WriteLine("5 - Adicionar linha ao assistente");
    Console.WriteLine("6 - Importar ficheiro de chamadas");
    Console.WriteLine("9 - Sair");
    Console.Write("\nDigite a opção desejada: ");

    opcaoDigitada = Console.ReadLine()!;
    if (!int.TryParse(opcaoDigitada, out opcao))
    {
        opcao = -1;
    }
    
    switch (opcao)
    {
        case 1:
            MenuAdicionarAssistente menuCadastrarAssistente = new();
            menuCadastrarAssistente.Executar(context);
            break;
        case 2:
            MenuListarAssistentes menuAssistente = new();
            menuAssistente.Executar(context);
            break;
        case 3:
            MenuDeletarAssistente menuDeletarAssistente = new();
            menuDeletarAssistente.Executar(context);
            break;
        case 4:
            MenuAdicionarChamada menuAdicionarChamada = new();
            menuAdicionarChamada.Executar(context);            
            break;
        case 5:
            MenuAdicionarLinha menuAdicionarLinha = new();
            menuAdicionarLinha.Executar(context);
            break;
        case 6:
            MenuImportarChamadas menuImportarChamadas = new();
            menuImportarChamadas.Executar(context);            
            break;
        case 9:
            return;
        default:
            Console.Write("Comando inválido!\nDigite uma das opções acima");
            Thread.Sleep(2000);
            break;
    }
}