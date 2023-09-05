using ConsolidadorDiario.Dominio.Projecoes;

namespace ConsolidadorDiario.Dominio.Interfaces;

public interface IBuscarConsolidadoDoDia
{
    Task<ConsolidadoDoDia?> Buscar(long caixaId, DateTime data);
}