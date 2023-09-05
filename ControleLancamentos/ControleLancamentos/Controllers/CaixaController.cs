using ControleLancamentos.Controllers.ViewModels;
using ControleLancamentos.Dominio.Interfaces.CasosDeUso;
using ControleLancamentos.Infraestrutura.SqlServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleLancamentos.Controllers;

[ApiController]
[Route("[controller]")]
public class CaixaController : ControllerBase
{
    private readonly ICriarCaixa _criarCaixa;
    private readonly IAdicionarLancamentoNoCaixa _adicionarLancamentoNoCaixa;
    private readonly CaixaContext _caixaContext;

    public CaixaController(ICriarCaixa criarCaixa,
        IAdicionarLancamentoNoCaixa adicionarLancamentoNoCaixa,
        CaixaContext caixaContext)
    {
        _criarCaixa = criarCaixa;
        _adicionarLancamentoNoCaixa = adicionarLancamentoNoCaixa;
        _caixaContext = caixaContext;
    }
    
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarOCaixaViewModel viewModel, CancellationToken cancellationToken)
    {
        return Ok(await _criarCaixa.Executar(viewModel.Nome, cancellationToken));
    }
    
    [HttpPost("{id:long}/lancamento")]
    public async Task<IActionResult> AdicionarNamento([FromBody] AdicionarLancamentoViewModel viewModel, [FromRoute] long id)
    {
        var result = await _adicionarLancamentoNoCaixa.Executar(id, viewModel);

        if (result)
            return NoContent();
        
        return BadRequest("Não foi possível adicionar o lançamento no caixa.");
    }
    
    [HttpGet]
    public async Task<IActionResult> ListaTodosOsCaixas(CancellationToken cancellationToken)
    {
        var list = await _caixaContext.Caixas.Include(item => item.Lancamentos).ToListAsync(cancellationToken);
        return Ok(await _caixaContext.Caixas.Include(item => item.Lancamentos).ToListAsync(cancellationToken));
    }
}