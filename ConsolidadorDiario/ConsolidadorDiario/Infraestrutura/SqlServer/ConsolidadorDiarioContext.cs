using ConsolidadorDiario.Dominio.AgregadorRaiz;
using Microsoft.EntityFrameworkCore;

namespace ConsolidadorDiario.Infraestrutura.SqlServer;

public class ConsolidadorDiarioContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Lancamento> Lancamentos { get; set; }

    public ConsolidadorDiarioContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_configuration.GetConnectionString("ConsolidadorDiario")).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Lancamento>(entity =>
        {
            entity.ToTable("Lancamento");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
            entity.Property(e => e.DataDeRegistro).IsRequired();
            entity.Property(e => e.TipoDoLancamento).IsRequired();
            entity.Property(e => e.Valor).IsRequired();
        });
        
        base.OnModelCreating(modelBuilder);
    }
}