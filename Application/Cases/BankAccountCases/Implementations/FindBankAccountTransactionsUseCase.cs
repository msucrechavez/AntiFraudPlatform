using Core.Domains;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManager.Application.Cases.BankAccountCases.Implementations
{
    public class FindBankAccountTransactionsUseCase: IFindBankAccountTransactionsUseCase
    {
        private readonly IBankTransactionRepository _bankTransactionRepository;

        public FindBankAccountTransactionsUseCase(IBankTransactionRepository bankTransactionRepository)
        {
            _bankTransactionRepository = bankTransactionRepository;
        }

        public Task<List<BankTransaction>> Execute(Guid bankAccountId)
        {
            return _bankTransactionRepository.GetByBankAccountId(bankAccountId);
        }

    }
}
