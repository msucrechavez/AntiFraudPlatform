using Application.Cases.BankAccountCases;
using Application.Cases.BankAccountCases.Implementations;
using Application.Cases.BankTransactionCases;
using Application.Cases.BankTransactionCases.Implementations;
using Confluent.Kafka;
using Core.Repositories;
using Core.Services;
using Infrastructure.Config;
using Infrastructure.Repositories_Impl;
using Infrastructure.Repository_Impl;
using Infrastructure.Services_Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TransactionManager.Application.Cases.BankAccountCases;
using TransactionManager.Application.Cases.BankAccountCases.Implementations;
using TransactionManager.Application.Cases.BankTransactionCases;
using TransactionManager.Application.Cases.BankTransactionCases.Implementations;
using TransactionManager.Core.Services;
using TransactionManager.Infrastructure.Services_Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlConnection"));
});

var producerConfig = new ProducerConfig
{
    BootstrapServers = "localhost:9092",
    ClientId = "txn-producer"
};

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "txn-consumer",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();

builder.Services.AddScoped<ICreateBankAccountUseCase, CreateBankAccountUseCase>();
builder.Services.AddScoped<IFindBankAccountTransactionsUseCase, FindBankAccountTransactionsUseCase>();
builder.Services.AddScoped<IFindSingleBankAccountUseCase, FindsingleBankAccountUseCase>();
builder.Services.AddScoped<IListAllBankAccountsUseCase, ListAllBankAccountsUseCase>();

builder.Services.AddScoped<ICreateBankTransactionUseCase, CreateBankTransactionUseCase>();
builder.Services.AddScoped<IUpdateBankTransactionUseCase, UpdateBankTransactionUseCase>();

builder.Services.AddSingleton(
    new ProducerBuilder<string, string>(producerConfig).Build());

builder.Services.AddSingleton<IBankTransactionEventProducerService, BankTransactionEventProducerService>();

builder.Services.AddSingleton(
    new ConsumerBuilder<string, string>(consumerConfig).Build());

builder.Services.AddHostedService<BankTransactionEventConsumerService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
