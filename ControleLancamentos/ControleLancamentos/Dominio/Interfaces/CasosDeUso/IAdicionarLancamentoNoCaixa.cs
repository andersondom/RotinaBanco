using ControleLancamentos.Dominio.Entidades;

namespace ControleLancamentos.Dominio.Interfaces.CasosDeUso;

public interface IAdicionarLancamentoNoCaixa
{
    Task<bool> Executar(long Id, Lancamento lancamento,  CancellationToken cancellationToken = default);
}