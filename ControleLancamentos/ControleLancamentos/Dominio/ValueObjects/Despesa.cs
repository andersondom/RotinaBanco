using ControleLancamentos.Dominio.Interfaces;

namespace ControleLancamentos.Dominio.ValueObjects;

public class Despesa : ITipoDoLancamento
{
    public int GetFatorParaMultiplicacao() => -1;

    public override string ToString() => nameof(Despesa).ToLower();
}