using ConsolidadorDiario.Dominio.AgregadorRaiz;
using ConsolidadorDiario.Dominio.Projecoes;

namespace ConsolidadorDiario.Dominio.Extensoes;

public static class ExtensaoDeLancamento
{
    public static ConsolidadoDoDia ToConsolidadoDoDia(this ICollection<Lancamento?> lancamentos) =>
        new ConsolidadoDoDia
        {
            CaixaId = lancamentos.First()!.CaixaId,
            Data =  lancamentos.First()!.DataDeRegistro,
            ValorTotal = lancamentos.Sum(e => e.Valor),
            ValorTotalDeEntradas = lancamentos.Where(e => e.Valor > 0).Sum(e => e.Valor),
            ValorTotalDeSaidas = lancamentos.Where(e => e.Valor < 0).Sum(e => e.Valor)
        };
}