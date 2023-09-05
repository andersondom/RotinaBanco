using ConsolidadorDiario.Dominio.AgregadorRaiz;
using ConsolidadorDiario.Dominio.EventosDeIntegracao;
using ConsolidadorDiario.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsolidadorDiario.Infraestrutura.SqlServer.Repositorios;

public class RepositorioDosLancamentos : IRepositorioDosLancamentos
{
    private readonly ConsolidadorDiarioContext _context;

    public RepositorioDosLancamentos(ConsolidadorDiarioContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Lancamento>> BuscarLancamentosPorData(long caixaId, DateTime data)
    {
        return await _context.Lancamentos
            .Where(e => e.DataDeRegistro.Date == data.Date && caixaId == e.CaixaId)
            .ToListAsync();
    }

    public async Task Inserir(LancamentoInserido contextMessage)
    {
        _context.Lancamentos.Add(new Lancamento()
        {
            DataDeRegistro = contextMessage.DataDeRegistro,
            Description = contextMessage.Description,
            TipoDoLancamento = contextMessage.TipoDoLancamento,
            Valor = contextMessage.Valor,
            CaixaId = contextMessage.CaixaId
        });
        await _context.SaveChangesAsync();
    }
}