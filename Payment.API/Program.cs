using MassTransit;
using MassTransit.SqlTransport;
using Payment.API.Consumers;
using Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMassTransit(congirator =>
{
    /* Olu�turulan consumer � belirtiyoruz */
    congirator.AddConsumer<StockReservedEventConsumer>();
    congirator.UsingRabbitMq((context, _configurator) =>
    {
        _configurator.Host(builder.Configuration["RabbitMQ"]);

        /* Bu consumer �n hangi kuyru�u dinleyece�ini belirtmemiz gerekiyor */
        _configurator.ReceiveEndpoint(RabbitMQSettings.Payment_StockReservedEventQueue, e => e.ConfigureConsumer<StockReservedEventConsumer>(context));
    });
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

app.Run();
