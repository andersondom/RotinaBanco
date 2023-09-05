using System.ComponentModel.DataAnnotations;

namespace ControleLancamentos.Controllers.ViewModels;

public class CriarOCaixaViewModel
{
    [Required, MinLength(10)]
    public string Nome { get; set; }
}