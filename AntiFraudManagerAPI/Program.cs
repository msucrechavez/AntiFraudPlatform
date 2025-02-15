using AntiFraud.Application.Cases.BankTransactionCases;
using AntiFraud.Application.Cases.BankTransactionCases.Implementations;
using AntiFraud.Application.Services;
using AntiFraud.Core.Repositories;
using AntiFraud.Core.Services;
using AntiFraud.Infrastructure.Repositories_Impl;
using AntiFraud.Infrastructure.Services_Impl;
using Confluent.Kafka;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

var producerConfig = new ProducerConfig
{
    BootstrapServers = "localhost:9092",
    ClientId = "antifraud-producer"
};

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "antifraud-consumer",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

// Add services to the container.

builder.Services.AddScoped<IBankAccountDailyAmountCacheRepository,BankAccountDailyAmountCacheRepository>();
builder.Services.AddScoped<IBankTransactionAntiFraudService, BankTarnsactionAntiFraudService>();
builder.Services.AddScoped<IVerifyBankAccountTransactionUseCase, VerifyBankAccountTransactionUseCase>();

builder.Services.AddScoped<IBankAccountDailyAmountCacheRepository, BankAccountDailyAmountCacheRepository>();

builder.Services.AddSingleton(
    new ProducerBuilder<string, string>(producerConfig).Build());

builder.Services.AddSingleton<IBankTransactionAntiFraudEventProducerService, BankTransactionAntiFraudEventProducerService>();

builder.Services.AddSingleton(
    new ConsumerBuilder<string, string>(consumerConfig).Build());

builder.Services.AddScoped<IBankTransactionAntiFraudEventConsumerService, BankTransactionAntiFraudEventConsumerService>();

builder.Services.AddHostedService<BankTransactionAntiFraudEventConsumerService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
