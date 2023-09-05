using System.ComponentModel.DataAnnotations;
using ConsolidadorDiario.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsolidadorDiario.Controllers;

[ApiController]
[Route("[controller]")]
public class LancamentosController : ControllerBase
{
    [HttpGet("{caixaId:long}/dia")]
    public async Task<IActionResult> Get([FromServices] IBuscarConsolidadoDoDia buscarConsolidadoDoDia, long caixaId, [FromQuery] [Required] DateTime data)
    {
        var resultado = await buscarConsolidadoDoDia.Buscar(caixaId, data); 
        return resultado == null ? NotFound($"Nao tem registro de lacamentos para esse caixa {caixaId}.") : Ok(resultado);
    }
}