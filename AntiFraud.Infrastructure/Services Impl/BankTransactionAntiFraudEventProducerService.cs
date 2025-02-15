using AntiFraud.Core.Events;
using AntiFraud.Core.Services;
using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Infrastructure.Services_Impl
{
    public class BankTransactionAntiFraudEventProducerService : IBankTransactionAntiFraudEventProducerService
    {
        private readonly IProducer<string, string> _producer;

        private const string TOPIC = "bank-antifraud-events";

        public BankTransactionAntiFraudEventProducerService(IProducer<string, string> producer)
        {
            _producer = producer;
        }

        public Task SendBankTransactionAntiFraudEvent(BankTransactionEvent bankTransactionEvent)
        {
            Console.WriteLine("BankTransactionAntiFraudEvent sent");
            var kafkaMessage = new Message<string, string>
            {
                Value = JsonConvert.SerializeObject(bankTransactionEvent)
            };
            return _producer.ProduceAsync(TOPIC, kafkaMessage);
        }
    }
}
