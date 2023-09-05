using ControleLancamentos.Dominio.AgregadorRaiz;
using ControleLancamentos.Dominio.Entidades;
using ControleLancamentos.Dominio.Interfaces;
using ControleLancamentos.Dominio.ValueObjects;
using ControleLancamentos.Infraestrutura.SqlServer.Converters;
using Microsoft.EntityFrameworkCore;

namespace ControleLancamentos.Infraestrutura.SqlServer;

public class CaixaContext : DbContext
{
    private readonly IConfiguration _configuration;
    public virtual DbSet<Caixa> Caixas { get; set; }
    public virtual DbSet<Lancamento> Lancamentos { get; set; }

    public CaixaContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("ControleDeLancamento"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Caixa>(entity =>
        {
            entity.ToTable("Caixa");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
            entity.Property(e => e.DataDeAbertura).IsRequired();
            entity.Property(e => e.DataDeFechamento).IsRequired(false);
            entity.Property(e => e.SituacaoDoCaixa).IsRequired();
            entity.HasMany(e => e.Lancamentos)
                .WithOne(e => e.Caixa).HasForeignKey(e => e.Id);
        });

        modelBuilder.Entity<Lancamento>(entity =>
        {
            entity.ToTable("Lancamento");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
            entity.Property(e => e.DataDeRegistro).IsRequired();
            entity.HasOne(e => e.Caixa)
                .WithMany(e => e.Lancamentos)
                .HasForeignKey(e => e.CaixaId);
            entity.Property(e => e.TipoDoLancamento).HasConversion(new TipoDoLancamentoConverter()).IsRequired();
            entity.Property(e => e.Valor).IsRequired();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}