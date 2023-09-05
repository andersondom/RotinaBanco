using ConsolidadorDiario.Dominio.Extensoes;
using ConsolidadorDiario.Dominio.Interfaces;
using ConsolidadorDiario.Dominio.Projecoes;

namespace ConsolidadorDiario.Aplicacao.CasosDeUso;

public class BuscarConsolidadoDoDia : IBuscarConsolidadoDoDia
{
    private readonly IRepositorioDosLancamentos _repositorioDosLancamentos;

    public BuscarConsolidadoDoDia(IRepositorioDosLancamentos repositorioDosLancamentos)
    {
        _repositorioDosLancamentos = repositorioDosLancamentos;
    }

    public async Task<ConsolidadoDoDia?> Buscar(long caixaId, DateTime data)
    {
        var lancamentos = await _repositorioDosLancamentos.BuscarLancamentosPorData(caixaId, data);

        return !lancamentos.Any() ? null : lancamentos!.ToConsolidadoDoDia();
    }
}