using Calculadora_de_TMA.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_de_TMA.Banco;

public class DAL<T> where T : class
{
    private readonly CalculadoraDeTmaContext context;

    public DAL(CalculadoraDeTmaContext context)
    {
        this.context = context;
    }

    #region Métodos Síncronos
    public void Adicionar(T obj)
    {
        context.Set<T>().Add(obj);
        context.SaveChanges();
    }
    public void Atualizar(T obj)
    {
        context.Set<T>().Update(obj);
        context.SaveChanges();
    }
    public void Remover(T obj)
    {
        context.Set<T>().Remove(obj);        
        context.SaveChanges();
    }
    public List<T> Listar()
    {
        return context.Set<T>().ToList();
    }
    public T? Buscar(Expression<Func<T, bool>> condicao)
    {
        return context.Set<T>().FirstOrDefault(condicao);
    }
    #endregion

    #region Métodos Assíncronos
    public async Task AdicionarAsync(T obj)
    {
        context.Set<T>().Add(obj);
        await context.SaveChangesAsync();
    }
    public async Task AtualizarAsync(T obj)
    {
        context.Set<T>().Update(obj);
        await context.SaveChangesAsync();
    }
    public async Task RemoverAsync(T obj)
    {
        context.Set<T>().Remove(obj);
        await context.SaveChangesAsync();
    }
    public async Task<List<T>> ListarAsync()
    {
        return await context.Set<T>().ToListAsync();
    }
    public async Task<T?> BuscarAsync(Expression<Func<T, bool>> condicao)
    {
        return await context.Set<T>().FirstOrDefaultAsync(condicao);
    }
    #endregion
}