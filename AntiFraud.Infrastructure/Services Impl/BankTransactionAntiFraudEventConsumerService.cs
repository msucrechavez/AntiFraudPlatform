using AntiFraud.Application.Cases.BankTransactionCases;
using AntiFraud.Core.Events;
using AntiFraud.Core.Services;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace AntiFraud.Infrastructure.Services_Impl
{
    public class BankTransactionAntiFraudEventConsumerService : IBankTransactionAntiFraudEventConsumerService, IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private const string TOPIC = "bank-transaction-events";
        public BankTransactionAntiFraudEventConsumerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public Task ProcessBankTransactionEvent(CancellationToken cancellationToken)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var _consumer = scope.ServiceProvider.GetRequiredService<IConsumer<string, string>>();
            var _verifyBankAccountTransactionUseCase = scope.ServiceProvider.GetRequiredService<IVerifyBankAccountTransactionUseCase>();

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
                    _verifyBankAccountTransactionUseCase.Execute(bankTransactionEvent.BankTransactionSummary);
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
