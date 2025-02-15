using AntiFraud.Core.Domains;
using AntiFraud.Core.Events;
using AntiFraud.Core.Repositories;
using AntiFraud.Core.Services;

namespace AntiFraud.Application.Cases.BankTransactionCases.Implementations
{
    public class VerifyBankAccountTransactionUseCase : IVerifyBankAccountTransactionUseCase
    {

        private readonly IBankTransactionAntiFraudEventProducerService _bankTransactionEventProducerService;

        private readonly IBankAccountDailyAmountCacheRepository _bankAccountDailyAmountCacheRepository;

        private readonly IBankTransactionAntiFraudService _bankTransacitonAntiFraudService;


        public VerifyBankAccountTransactionUseCase(IBankTransactionAntiFraudEventProducerService bankTransactionEventProducerService,
            IBankAccountDailyAmountCacheRepository bankAccountDailyAmountCacheRepository,
            IBankTransactionAntiFraudService bankTransacitonAntiFraudService)
        {
            _bankTransactionEventProducerService = bankTransactionEventProducerService;
            _bankAccountDailyAmountCacheRepository = bankAccountDailyAmountCacheRepository;
            _bankTransacitonAntiFraudService = bankTransacitonAntiFraudService;
        }

        public async Task Execute(BankTransactionSummary bankTransactionSummary)
        {
            var bankAccountDailyAmount = await _bankAccountDailyAmountCacheRepository.GetById(bankTransactionSummary.SourceBankAccountId);
            bool create = false;
            if (bankAccountDailyAmount == null)
            {
                bankAccountDailyAmount = new BankAccountDailyAmount
                {
                    BankAccountId = bankTransactionSummary.SourceBankAccountId,
                    DailyAmount = 0
                };
                create = true;
            }

            var isValid = _bankTransacitonAntiFraudService.IsValid(bankTransactionSummary, bankAccountDailyAmount);
            
            if (isValid)
            {
                bankAccountDailyAmount.DailyAmount += bankTransactionSummary.Amount;
                if (create)
                {
                    await _bankAccountDailyAmountCacheRepository.Create(bankAccountDailyAmount);
                }
                await _bankAccountDailyAmountCacheRepository.Update(bankAccountDailyAmount);
            }

            var bankTransactionEvent = new BankTransactionEvent
            {
                Type = "BankTransactionEvent",
                BankTransactionSummary = bankTransactionSummary,
                IsValid = isValid
            };
            _bankTransactionEventProducerService.SendBankTransactionAntiFraudEvent(bankTransactionEvent);
        }
    }
}
