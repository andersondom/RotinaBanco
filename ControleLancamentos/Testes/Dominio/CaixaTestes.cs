using ControleLancamentos.Dominio.AgregadorRaiz;
using ControleLancamentos.Dominio.Entidades;
using ControleLancamentos.Dominio.Enumeradores;
using ControleLancamentos.Dominio.ValueObjects;

namespace Testes.Dominio;

public class CaixaTestes
{
 
    [Fact]
    public void DadoUmaCaixa_QuandoFechada_EntaoMudaOStatusParaFechada()
    {
        // Arrange
        var caixa = new Caixa();
        caixa.Abrir("Caixa de Teste");
        
        // Act
        caixa.Fechar();
        
        // Assert
        Assert.Equal(SituacaoDoCaixa.Fechado, caixa.SituacaoDoCaixa);
        Assert.True(caixa.DataDeFechamento <= DateTime.Now);
    }


    [Fact]
    public void DadoUmaCaixa_QuandoAbertaPelaPrimeiraVez_EntaoOStatuseAberta()
    {
        // Arrange
        var caixa = new Caixa();
        
        // Act
        caixa.Abrir("Caixa de Teste");
        
        // Assert
        Assert.Equal(SituacaoDoCaixa.Aberto, caixa.SituacaoDoCaixa);
        Assert.Equal("Caixa de Teste", caixa.Nome);
        Assert.True(caixa.DataDeAbertura <= DateTime.Now);
    }
    
    
    [Fact]
    public void DadoUmaCaixa_QuandoAdicionadoUmLancamento_EntaoAumentaAquantideDeRegistros()
    {
        // Arrange
        var caixa = new Caixa();
        
        // Act
        caixa.Abrir("Caixa de Teste");
        caixa.AdicionarLancamento(new Lancamento("teste lancamento", 100,  new Receita()));
        
        // Assert
        Assert.Equal(SituacaoDoCaixa.Aberto, caixa.SituacaoDoCaixa);
        Assert.Equal("Caixa de Teste", caixa.Nome);
        Assert.True(caixa.DataDeAbertura <= DateTime.Now);
        Assert.Single(caixa.Lancamentos);
        Assert.Equal(100,caixa.Lancamentos.Single().Valor);
        Assert.Equal("teste lancamento", caixa.Lancamentos.Single().Description);
    }
}