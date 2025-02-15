using Core.Domains;
using Core.Repositories;

namespace Application.Cases.BankAccountCases.Implementations
{
    public class CreateBankAccountUseCase : ICreateBankAccountUseCase
    {

        private readonly IBankAccountRepository _bankAccountRepository;
        public CreateBankAccountUseCase(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public Task<BankAccount> Execute(BankAccount bankAccount)
        {
            bankAccount.Balance = 0;
            bankAccount.Created = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return _bankAccountRepository.Create(bankAccount);
        }
    }
}
