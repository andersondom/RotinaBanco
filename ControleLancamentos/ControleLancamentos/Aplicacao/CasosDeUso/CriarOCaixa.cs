using ControleLancamentos.Dominio.AgregadorRaiz;
using ControleLancamentos.Dominio.Interfaces.CasosDeUso;
using ControleLancamentos.Infraestrutura.SqlServer;

namespace ControleLancamentos.Aplicacao.CasosDeUso;

public class CriarOCaixa : ICriarCaixa
{
    private readonly CaixaContext _caixaContext;

    public CriarOCaixa(CaixaContext caixaContext) => _caixaContext = caixaContext;

    public async Task<Caixa> Executar(string nome, CancellationToken cancellationToken = default)
    {
        var caixa = new Caixa();
        caixa.Abrir(nome);
        
          _caixaContext.Caixas.Add(caixa);
         await _caixaContext.SaveChangesAsync(cancellationToken);

         return caixa;
    }
}