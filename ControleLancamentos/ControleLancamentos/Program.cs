using System.Net.Mime;
using System.Text.Json.Serialization;
using ControleLancamentos.Aplicacao.CasosDeUso;
using ControleLancamentos.Dominio.EventosDeDominio;
using ControleLancamentos.Dominio.Interfaces.CasosDeUso;
using ControleLancamentos.Infraestrutura.SqlServer;
using MassTransit;
using RabbitMQ.Client;

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

builder.Services.AddScoped<ICriarCaixa, CriarOCaixa>();
builder.Services.AddScoped<IAdicionarLancamentoNoCaixa, AdicionarLancamentoNoCaixa>();
builder.Services.AddDbContext<CaixaContext>();

var rabbitmqOptions = new RabbitMqOptions();
builder.Configuration.Bind("RabbitMq", rabbitmqOptions);

builder.Services.AddMassTransit(configure =>
{
    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.UseNewtonsoftRawJsonSerializer();
        configurator.UseNewtonsoftRawJsonDeserializer();
        configurator.Host(rabbitmqOptions.Host, rabbitmqOptions.VirtualHost, h =>
        {
            h.Username(rabbitmqOptions.Username);
            h.Password(rabbitmqOptions.Password);
            h.Heartbeat(5);
            h.PublisherConfirmation = true;
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