using Core.Domains;

namespace Application.Cases.BankTransactionCases
{
    public interface ICreateBankTransactionUseCase
    {
        Task<BankTransaction> Execute(BankTransaction bankTransaction);
    }
}
