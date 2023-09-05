using ControleLancamentos.Dominio.Entidades;
using ControleLancamentos.Dominio.Enumeradores;

namespace ControleLancamentos.Dominio.AgregadorRaiz;

public class Caixa
{
    public long Id { get; }
    public string Nome { get; private set; }
    public DateTime DataDeAbertura { get; private set; }
    public DateTime? DataDeFechamento { get; private set; }
    public ICollection<Lancamento> Lancamentos { get; private set; }
    public SituacaoDoCaixa SituacaoDoCaixa { get; private set; }

    public Caixa()
    {
        Lancamentos = new List<Lancamento>();
    }
    
    public void AdicionarLancamento(Lancamento lancamento)
    {
        Lancamentos.Add(lancamento);
    }
    
    public void Abrir(string nome)
    {
        Nome = nome;
        DataDeAbertura = DateTime.Now;
        SituacaoDoCaixa = SituacaoDoCaixa.Aberto;
    }
    
    public void Fechar()
    {
        SituacaoDoCaixa = SituacaoDoCaixa.Fechado;
        DataDeFechamento = DateTime.Now;
    }
    
}