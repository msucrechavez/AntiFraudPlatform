using Core.Domains;
using TransactionManager.Core.Domains;

namespace TransactionManager.Core.Events
{
    public class BankTransactionEvent
    {
        public required BankTransactionSummary BankTransactionSummary { get; set; }

        public bool IsValid { get; set; }
    }
}