namespace ControleLancamentos.Dominio.EventosDeDominio;

public record LancamentoInserido(long Id, string Description, DateTime DataDeRegistro, decimal Valor,
    string TipoDoLancamento, long CaixaId);
