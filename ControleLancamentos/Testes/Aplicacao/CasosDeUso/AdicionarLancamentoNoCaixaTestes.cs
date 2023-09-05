using ControleLancamentos.Aplicacao.CasosDeUso;
using ControleLancamentos.Dominio.AgregadorRaiz;
using ControleLancamentos.Dominio.Entidades;
using ControleLancamentos.Dominio.EventosDeDominio;
using ControleLancamentos.Dominio.ValueObjects;
using ControleLancamentos.Infraestrutura.SqlServer;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Testes.Aplicacao.CasosDeUso;

public class AdicionarLancamentoNoCaixaTestes
{
    private readonly  Mock<CaixaContext> _caixaContext;
    private readonly Mock<IBus> _bus;
    private AdicionarLancamentoNoCaixa _adicionarLancamentoNoCaixa;
    
    public AdicionarLancamentoNoCaixaTestes()
    {
        var configuration = new Mock<IConfiguration>();
        _caixaContext = new Mock<CaixaContext>(configuration.Object);
        var dbSet = new Mock<DbSet<Caixa>>();
        _caixaContext.Setup(x => x.Caixas).Returns(dbSet.Object);
        _bus = new Mock<IBus>();
        _adicionarLancamentoNoCaixa = new AdicionarLancamentoNoCaixa(_caixaContext.Object, _bus.Object);
    }

    [Fact]
    public async Task DadoUmLancamento_QuandoExecutado_EntaoAdicionadoNoCaixa()
    {
        // Arrange
        var caixa = new Caixa();
        caixa.Abrir("Teste de adição de lançamento");
        
        _caixaContext.Setup(x => x.Caixas.FindAsync(It.IsAny<object[]?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(caixa);
        _bus.Setup(item => item.Publish(It.IsAny<LancamentoInserido>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        
        // Act
        var resultado = await _adicionarLancamentoNoCaixa.Executar(caixa.Id, new Lancamento("Teste", 100, new Receita()));
        
        // Assert
        Assert.True(resultado);
        Assert.Single(caixa.Lancamentos);
    } 
    
    [Fact]
    public async Task DadoUmLancamento_QuandoACaixaNaoExiste_EntaoNaoAdiciona()
    {
        // Arrange
        _caixaContext.Setup(x => x.Caixas.FindAsync(It.IsAny<object[]?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Caixa?)null);
        
        // Act
        var resultado = await _adicionarLancamentoNoCaixa.Executar(10, new Lancamento("Teste", 100, new Receita()));
        
        // Assert
        Assert.False(resultado);
    } 
}