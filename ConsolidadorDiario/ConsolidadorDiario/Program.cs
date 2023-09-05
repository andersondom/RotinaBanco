using System.Text.Json.Serialization;
using ConsolidadorDiario.Aplicacao.CasosDeUso;
using ConsolidadorDiario.Dominio.Interfaces;
using ConsolidadorDiario.Infraestrutura.Rabbitmq.Consumers;
using ConsolidadorDiario.Infraestrutura.SqlServer;
using ConsolidadorDiario.Infraestrutura.SqlServer.Repositorios;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBuscarConsolidadoDoDia, BuscarConsolidadoDoDia>();
builder.Services.AddScoped<IRepositorioDosLancamentos, RepositorioDosLancamentos>();

builder.Services.AddDbContext<ConsolidadorDiarioContext>();

var rabbitmqOptions = new RabbitMqOptions();
builder.Configuration.Bind("RabbitMq", rabbitmqOptions);

builder.Services.AddMassTransit(configure =>
{
    configure.SetKebabCaseEndpointNameFormatter();
    configure.AddConsumer<ConsumidorDeLancamentoInserido>();
    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.UseNewtonsoftRawJsonSerializer();
        configurator.UseNewtonsoftRawJsonDeserializer();
        configurator.UseMessageRetry(x => x.Interval(2, TimeSpan.FromSeconds(10)));
        configurator.Host(rabbitmqOptions.Host, rabbitmqOptions.VirtualHost, h =>
        {
            h.Username(rabbitmqOptions.Username);
            h.Password(rabbitmqOptions.Password);
            h.Heartbeat(5);
        });
        
        configurator.ReceiveEndpoint(KebabCaseEndpointNameFormatter.Instance.Message<ConsumidorDeLancamentoInserido>(),endpoint =>
        {
            configurator.UseNewtonsoftRawJsonSerializer();
            configurator.UseNewtonsoftRawJsonDeserializer();
            endpoint.ConfigureConsumer<ConsumidorDeLancamentoInserido>(context);
            endpoint.Bind("ControleLancamentos.Dominio.EventosDeDominio:LancamentoInserido");
            endpoint.ConfigureConsumeTopology = false;
        });
        
        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddOptions<MassTransitHostOptions>()
    .Configure(options =>
    {
        options.WaitUntilStarted = true;
        options.StartTimeout = TimeSpan.FromSeconds(10);
        options.StopTimeout = TimeSpan.FromSeconds(10);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

public class RabbitMqOptions
{
    public string Host { get; set; }
    public string VirtualHost { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}