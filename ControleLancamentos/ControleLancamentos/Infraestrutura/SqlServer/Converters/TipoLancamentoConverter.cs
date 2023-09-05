using ControleLancamentos.Dominio.Interfaces;
using ControleLancamentos.Dominio.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ControleLancamentos.Infraestrutura.SqlServer.Converters;

public class TipoDoLancamentoConverter : ValueConverter<ITipoDoLancamento, int>
{
    public TipoDoLancamentoConverter(ConverterMappingHints mappingHints = null)
        : base(
            v => v.GetFatorParaMultiplicacao(), // Convert ITipoDoLancamento to discriminator string
            v => CreateInstanceByDiscriminator(v), // Convert string back to ITipoDoLancamento
            mappingHints
        )
    {
    }

    private static ITipoDoLancamento CreateInstanceByDiscriminator(int value)
    {
        if (value < 0)
        {
            return new Despesa();
        }
        
        return new Receita();
    }
}
