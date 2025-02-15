using Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManager.Application.Cases.BankAccountCases
{
    public interface IFindBankAccountTransactionsUseCase
    {
        Task<List<BankTransaction>> Execute(Guid bankAccountId);
    }
}
