using ControleLancamentos.Dominio.AgregadorRaiz;
using ControleLancamentos.Dominio.Interfaces;

namespace ControleLancamentos.Dominio.Entidades;

public class Lancamento
{
    public long Id { get; set; }
    public string Description { get; set; }
    public DateTime DataDeRegistro { get; set; }
    public decimal Valor { get; set; }
    public ITipoDoLancamento TipoDoLancamento { get; set; }
    public Caixa Caixa { get; }
    public long CaixaId { get; set; }

    public Lancamento(string description, decimal valor, ITipoDoLancamento tipoDoLancamento)
    {
        Description = description;
        Valor = valor * tipoDoLancamento.GetFatorParaMultiplicacao();
        TipoDoLancamento = tipoDoLancamento;
        DataDeRegistro = DateTime.Now;
    }
}