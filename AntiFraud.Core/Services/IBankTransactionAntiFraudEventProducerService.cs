using AntiFraud.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Core.Services
{
    public interface IBankTransactionAntiFraudEventProducerService
    {
        public Task SendBankTransactionAntiFraudEvent(BankTransactionEvent bankTransactionEvent);
    }
}
