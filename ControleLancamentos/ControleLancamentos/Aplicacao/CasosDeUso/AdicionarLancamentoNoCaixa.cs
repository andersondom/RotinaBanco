using ControleLancamentos.Dominio.Entidades;
using ControleLancamentos.Dominio.EventosDeDominio;
using ControleLancamentos.Dominio.Interfaces.CasosDeUso;
using ControleLancamentos.Infraestrutura.SqlServer;
using MassTransit;

namespace ControleLancamentos.Aplicacao.CasosDeUso;

public class AdicionarLancamentoNoCaixa : IAdicionarLancamentoNoCaixa
{
    private readonly CaixaContext _caixaContext;
    private readonly IBus _bus;

    public AdicionarLancamentoNoCaixa(CaixaContext caixaContext, IBus bus)
    {
        _caixaContext = caixaContext;
        _bus = bus;
    }
    
    public async Task<bool> Executar(long Id, Lancamento lancamento, CancellationToken cancellationToken = default)
    {
        var caixa = await _caixaContext.Caixas.FindAsync(new object?[] { Id }, cancellationToken: cancellationToken);

        if (caixa == null) return false;
        
        caixa.AdicionarLancamento(lancamento);
        _caixaContext.Caixas.Update(caixa);
        await _caixaContext.SaveChangesAsync(cancellationToken);
        
        LancamentoInserido lancamentoInserido = new(lancamento.Id,
            lancamento.Description, 
            lancamento.DataDeRegistro,
            lancamento.Valor,
            lancamento.TipoDoLancamento.ToString(),
            caixa.Id);
        
        await _bus.Publish(lancamentoInserido, cancellationToken);
        
        return true;
    }
}