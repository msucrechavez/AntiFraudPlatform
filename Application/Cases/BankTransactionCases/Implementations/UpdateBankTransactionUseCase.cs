using Core.Domains;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionManager.Core.Domains;
using TransactionManager.Core.Events;

namespace TransactionManager.Application.Cases.BankTransactionCases.Implementations
{
    public class UpdateBankTransactionUseCase : IUpdateBankTransactionUseCase
    {
        private readonly IBankTransactionRepository bankTransactionRepository;

        private readonly IBankAccountRepository bankAccountRepository;

        public UpdateBankTransactionUseCase(IBankTransactionRepository bankTransactionRepository, IBankAccountRepository bankAccountRepository)
        {
            this.bankTransactionRepository = bankTransactionRepository;
            this.bankAccountRepository = bankAccountRepository;
        }

        public async Task Execute(BankTransactionSummary bankTransactionSummary, bool IsValid)
        {
            var bankTransaction = await bankTransactionRepository.GetById(bankTransactionSummary.Id);
            
            bankTransaction.Status = IsValid ? TransactionStatus.Approved.ToString() : TransactionStatus.Rejected.ToString();
            await bankTransactionRepository.Update(bankTransaction);
            
            if (IsValid)
            {
                var bankAccount = await bankAccountRepository.GetById(bankTransaction.SourceBankAccountId);
                bankAccount.Balance += bankTransaction.Amount;

                await bankAccountRepository.Update(bankAccount);
            }

        }
    }
}