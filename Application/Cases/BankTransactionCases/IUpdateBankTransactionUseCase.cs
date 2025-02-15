using TransactionManager.Core.Domains;
using TransactionManager.Core.Events;

namespace TransactionManager.Application.Cases.BankTransactionCases
{
    public interface IUpdateBankTransactionUseCase
    {
        public Task Execute(BankTransactionSummary bankTransactionSummary, bool IsValid);
    }
}
