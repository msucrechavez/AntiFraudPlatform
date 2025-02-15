using Application.Cases.BankAccountCases.Implementations;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TransactionManager.Application.Cases.BankAccountCases;
using TransactionManager.Application.Cases.BankTransactionCases;
using TransactionManager.Application.Cases.BankTransactionCases.Implementations;
using TransactionManager.Core.Events;
using TransactionManager.Core.Services;
using static Confluent.Kafka.ConfigPropertyNames;

namespace TransactionManager.Infrastructure.Services_Impl
{
    public class BankTransactionEventConsumerService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private const string TOPIC = "bank-antifraud-events";

        public BankTransactionEventConsumerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task ProcessBankTransactionEvent(CancellationToken cancellationToken)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var _consumer = scope.ServiceProvider.GetRequiredService<IConsumer<string, string>>();
            var _updateBankTransactionUseCase = scope.ServiceProvider.GetRequiredService<IUpdateBankTransactionUseCase>();

            _consumer.Subscribe(TOPIC);
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    if (consumeResult is null)
                    {
                        return;
                    }
                    Console.WriteLine("message: " + consumeResult.Message.Value);
                    var bankTransactionEvent = JsonConvert.DeserializeObject<BankTransactionEvent>(consumeResult.Message.Value);
                    _updateBankTransactionUseCase.Execute(bankTransactionEvent.BankTransactionSummary, bankTransactionEvent.IsValid);
                }
            }, cancellationToken);
            return Task.CompletedTask;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return ProcessBankTransactionEvent(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
