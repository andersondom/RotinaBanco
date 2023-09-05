using System.ComponentModel.DataAnnotations;
using ControleLancamentos.Dominio.Entidades;
using ControleLancamentos.Dominio.ValueObjects;

namespace ControleLancamentos.Controllers.ViewModels;

public class AdicionarLancamentoViewModel
{
    [Required, MaxLength(100)]
    public string Description { get; set; }
    
    [Required, Range(0.01, double.MaxValue)]
    public decimal Valor { get; set; }
    
    [Required, RegularExpression("^(receita|despesa)$")]
    public string TipoDoLancamento { get; set; }

    public static implicit operator Lancamento(AdicionarLancamentoViewModel dto)
    {
        return new Lancamento(dto.Description, dto.Valor, dto.TipoDoLancamento switch
        {
            "receita" => new Receita(),
            "despesa" => new Despesa(),
            _ => throw new ArgumentOutOfRangeException()
        });
    }
}