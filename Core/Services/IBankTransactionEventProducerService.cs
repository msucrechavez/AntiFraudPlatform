using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionManager.Core.Events;

namespace Core.Services
{
    public interface IBankTransactionEventProducerService
    {
        public Task SendBankTransactionEvent(BankTransactionEvent bankTransactionEvent);
    }
}