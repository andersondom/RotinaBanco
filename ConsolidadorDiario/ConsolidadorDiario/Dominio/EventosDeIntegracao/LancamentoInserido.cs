using MassTransit;

namespace ConsolidadorDiario.Dominio.EventosDeIntegracao;

[MessageUrn("ControleLancamentos.Dominio.EventosDeDominio:LancamentoInserido")]
public record LancamentoInserido(long Id, string Description, DateTime DataDeRegistro, decimal Valor, string TipoDoLancamento, long CaixaId);
