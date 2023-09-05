namespace ConsolidadorDiario.Dominio.Projecoes;

public record ConsolidadoDoDia
{
    public long CaixaId { get; init; }
    public DateTime Data { get; init; }
    public decimal ValorTotal { get; init; }
    public decimal ValorTotalDeEntradas { get; init; }
    public decimal ValorTotalDeSaidas { get; init; }
}