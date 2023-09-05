using ConsolidadorDiario.Dominio.EventosDeIntegracao;
using ConsolidadorDiario.Dominio.Interfaces;
using MassTransit;

namespace ConsolidadorDiario.Infraestrutura.Rabbitmq.Consumers;

public class ConsumidorDeLancamentoInserido : IConsumer<LancamentoInserido>
{
    private readonly IRepositorioDosLancamentos _repositorioDosLancamentos;

    public ConsumidorDeLancamentoInserido(IRepositorioDosLancamentos repositorioDosLancamentos) => _repositorioDosLancamentos = repositorioDosLancamentos;

    public Task Consume(ConsumeContext<LancamentoInserido> context) => _repositorioDosLancamentos.Inserir(context.Message);
}