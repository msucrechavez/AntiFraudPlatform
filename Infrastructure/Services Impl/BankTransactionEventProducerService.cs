using Confluent.Kafka;
using Core.Services;
using Newtonsoft.Json;
using TransactionManager.Core.Events;

namespace Infrastructure.Services_Impl
{
    public class BankTransactionEventProducerService : IBankTransactionEventProducerService
    {
        private readonly IProducer<string, string> _producer;

        private const string TOPIC = "bank-transaction-events";

        public BankTransactionEventProducerService(IProducer<string, string> producer)
        {
            _producer = producer;
        }

        public Task SendBankTransactionEvent(BankTransactionEvent bankTransactionEvent)
        {
            Console.WriteLine("CreateBankTransactionEvent sent");
            var kafkaMessage = new Message<string, string>
            {
                Value = JsonConvert.SerializeObject(bankTransactionEvent)
            };
            return _producer.ProduceAsync(TOPIC, kafkaMessage);
        }
    }
}
