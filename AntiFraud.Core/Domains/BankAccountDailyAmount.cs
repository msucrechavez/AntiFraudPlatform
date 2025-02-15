namespace AntiFraud.Core.Domains
{
    public class BankAccountDailyAmount
    {
        public Guid BankAccountId { get; set; }

        public float DailyAmount { get; set; }
    }
}
