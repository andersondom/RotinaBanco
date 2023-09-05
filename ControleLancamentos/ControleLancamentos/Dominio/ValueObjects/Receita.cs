using ControleLancamentos.Dominio.Interfaces;

namespace ControleLancamentos.Dominio.ValueObjects;

public class Receita : ITipoDoLancamento
{
    public int GetFatorParaMultiplicacao() => 1;
    public override string ToString() => nameof(Receita).ToLower();
}