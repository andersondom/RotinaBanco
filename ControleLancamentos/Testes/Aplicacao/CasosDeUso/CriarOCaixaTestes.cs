using ControleLancamentos.Aplicacao.CasosDeUso;
using ControleLancamentos.Dominio.AgregadorRaiz;
using ControleLancamentos.Infraestrutura.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Testes.Aplicacao.CasosDeUso;

public class CriarOCaixaTestes
{
    private readonly  Mock<CaixaContext> _caixaContext;
    private CriarOCaixa _criaOCaixa;
    
    public CriarOCaixaTestes()
    {
        var configuration = new Mock<IConfiguration>();
        _caixaContext = new Mock<CaixaContext>(configuration.Object);
        var dbSet = new Mock<DbSet<Caixa>>();
        _caixaContext.Setup(x => x.Caixas).Returns(dbSet.Object);
        _criaOCaixa = new CriarOCaixa(_caixaContext.Object);
    }
    
    [Fact]
    public async Task DadoUmaCaixa_QuandoExecutar_EntaoCrie()
    {
        // Arrange
        var nome = "Caixa 1";

        // Act
        var resultado = await _criaOCaixa.Executar(nome);
        
        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(nome, resultado.Nome);
        _caixaContext.Verify(m => m.Caixas.Add(It.IsAny<Caixa>()), Times.Once);
        _caixaContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}