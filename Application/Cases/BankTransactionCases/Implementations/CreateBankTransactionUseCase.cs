using Core.Domains;
using Core.Repositories;
using Core.Services;
using TransactionManager.Core.Domains;
using TransactionManager.Core.Events;

namespace Application.Cases.BankTransactionCases.Implementations
{
    public class CreateBankTransactionUseCase : ICreateBankTransactionUseCase
    {

        private readonly IBankTransactionRepository _bankTransactionRepository;

        private readonly IBankTransactionEventProducerService _bankTransactionEventProducerService;

        public CreateBankTransactionUseCase(IBankTransactionRepository bankTransactionRepository,
            IBankTransactionEventProducerService bankTransactionEventProducerService)
        {
            _bankTransactionRepository = bankTransactionRepository;
            _bankTransactionEventProducerService = bankTransactionEventProducerService;
        }

        public async Task<BankTransaction> Execute(BankTransaction bankTransaction)
        {
            bankTransaction.Created = bankTransaction.LastModified = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            bankTransaction.Type = "DEPOSIT";
            bankTransaction.Status = TransactionStatus.Pending.ToString();

            await _bankTransactionRepository.Create(bankTransaction);

            var bankTransactionSummary = new BankTransactionSummary
            {
                Id = bankTransaction.Id,
                SourceBankAccountId = bankTransaction.SourceBankAccountId,
                Amount = bankTransaction.Amount
            };

            var bankTransactionEvent = new BankTransactionEvent
            {
                BankTransactionSummary = bankTransactionSummary
            };
            _bankTransactionEventProducerService.SendBankTransactionEvent(bankTransactionEvent);

            return bankTransaction;
        }
    }
}
