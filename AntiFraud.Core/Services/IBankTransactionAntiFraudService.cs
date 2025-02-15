using AntiFraud.Core.Domains;

namespace AntiFraud.Core.Services
{
    public interface IBankTransactionAntiFraudService
    {
        bool IsValid(BankTransactionSummary bankTransaction, BankAccountDailyAmount bankAccountDailyAmount);
    }
}
