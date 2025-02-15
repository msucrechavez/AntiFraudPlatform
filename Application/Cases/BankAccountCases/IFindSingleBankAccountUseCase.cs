using Core.Domains;

namespace Application.Cases.BankAccountCases
{
    public interface IFindSingleBankAccountUseCase
    {
        Task<BankAccount> Execute(Guid id);
    }
}