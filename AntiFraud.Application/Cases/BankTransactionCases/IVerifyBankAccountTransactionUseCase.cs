using AntiFraud.Core.Domains;

namespace AntiFraud.Application.Cases.BankTransactionCases
{
    public interface IVerifyBankAccountTransactionUseCase
    {
        public Task Execute(BankTransactionSummary bankTransactionSummary);
    }
}
