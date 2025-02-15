using Core.Domains;

namespace Application.Cases.BankAccountCases
{
    public interface ICreateBankAccountUseCase
    {
        Task<BankAccount> Execute(BankAccount bankACcount);
    }
}