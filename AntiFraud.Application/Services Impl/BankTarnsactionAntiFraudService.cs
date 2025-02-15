using AntiFraud.Core.Domains;
using AntiFraud.Core.Services;

namespace AntiFraud.Application.Services
{
    public class BankTarnsactionAntiFraudService : IBankTransactionAntiFraudService
    {
        private readonly float ALLOWED_TRANSACTION_AMOUNT = 2000;
        private readonly float ALLOWED_DAILY_AMOUNT = 20_000;

        public bool IsValid(BankTransactionSummary bankTransaction, BankAccountDailyAmount bankAccountDailyAmount)
        {
            return !(bankTransaction.Amount > ALLOWED_TRANSACTION_AMOUNT ||
                bankAccountDailyAmount.DailyAmount + bankTransaction.Amount > ALLOWED_DAILY_AMOUNT);
        }
    }
}
