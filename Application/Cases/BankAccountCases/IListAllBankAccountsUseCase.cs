using Core.Domains;

namespace Application.Cases.BankAccountCases
{
    public interface IListAllBankAccountsUseCase
    {
        Task<List<BankAccount>> Execute();
    }
}