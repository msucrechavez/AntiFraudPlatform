using Core.Domains;
using Core.Repositories;

namespace Application.Cases.BankAccountCases.Implementations
{
    public class FindsingleBankAccountUseCase : IFindSingleBankAccountUseCase
    {
        private readonly IBankAccountRepository _bankAccountRepository;

        public FindsingleBankAccountUseCase(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public Task<BankAccount> Execute(Guid id)
        {
            return _bankAccountRepository.GetById(id);
        }
    }
}
