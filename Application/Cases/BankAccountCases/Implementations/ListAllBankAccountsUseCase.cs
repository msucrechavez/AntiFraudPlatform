using Core.Domains;
using Core.Repositories;

namespace Application.Cases.BankAccountCases.Implementations
{
    public class ListAllBankAccountsUseCase : IListAllBankAccountsUseCase
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public ListAllBankAccountsUseCase(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public Task<List<BankAccount>> Execute()
        {
            return _bankAccountRepository.GetAll();
        }
    }
}
