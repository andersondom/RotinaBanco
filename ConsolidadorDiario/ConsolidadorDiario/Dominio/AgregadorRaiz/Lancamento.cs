using ConsolidadorDiario.Dominio.Projecoes;

namespace ConsolidadorDiario.Dominio.AgregadorRaiz;

public class Lancamento
{
    public long Id { get; set; }
    public string Description { get; set; }
    public DateTime DataDeRegistro { get; set; }
    public decimal Valor { get; set; }
    public string TipoDoLancamento { get; set; }
    public long CaixaId { get; set; }
}