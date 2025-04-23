using BI_TMA.Shared.Models.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BI_TMA.Shared.DB.Banco;

public class BI_TMAContext : DbContext
{    
    public DbSet<Assistente> Assistentes { get; set; }
    public DbSet<Chamada> Chamadas { get; set; }
    public DbSet<Linha> Linhas { get; set; }
    public DbSet<LinhaAssistente> LinhasAssistentes { get; set; }



    private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = CalculadoraDeTMA; Integrated Security = True; Connect Timeout = 0; Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";

    public BI_TMAContext(DbContextOptions<BI_TMAContext> options) : base(options)
    {
    }

    public BI_TMAContext()
    {
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar relação Chamada - Assistente
        modelBuilder.Entity<Chamada>()
            .HasOne(c => c.Assistente)
            .WithMany(a => a.Chamadas)
            .HasForeignKey(c => c.AssistenteId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar relação Chamada - Linha
        modelBuilder.Entity<Chamada>()
            .HasOne(c => c.Linha)
            .WithMany(l => l.Chamadas)
            .HasForeignKey(c => c.LinhaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configurar relação muitos-para-muitos Assistente - Linha
        modelBuilder.Entity<LinhaAssistente>()
            .HasKey(la => new { la.AssistenteId, la.LinhaId });

        modelBuilder.Entity<LinhaAssistente>()
            .HasOne(la => la.Assistente)
            .WithMany(a => a.LinhasAssistentes)
            .HasForeignKey(la => la.AssistenteId);

        modelBuilder.Entity<LinhaAssistente>()
            .HasOne(la => la.Linha)
            .WithMany(l => l.LinhasAssistentes)
            .HasForeignKey(la => la.LinhaId);

    }
}
