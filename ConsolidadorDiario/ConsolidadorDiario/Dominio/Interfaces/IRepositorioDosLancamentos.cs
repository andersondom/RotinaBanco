using ConsolidadorDiario.Dominio.AgregadorRaiz;
using ConsolidadorDiario.Dominio.EventosDeIntegracao;

namespace ConsolidadorDiario.Dominio.Interfaces;

public interface IRepositorioDosLancamentos
{
    Task<ICollection<Lancamento>> BuscarLancamentosPorData(long caixaId, DateTime data);
    Task Inserir(LancamentoInserido contextMessage);
}