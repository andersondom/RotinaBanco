using ControleLancamentos.Dominio.AgregadorRaiz;

namespace ControleLancamentos.Dominio.Interfaces.CasosDeUso;

public interface ICriarCaixa
{
    public Task<Caixa> Executar(string nome, CancellationToken cancellationToken = default);
}