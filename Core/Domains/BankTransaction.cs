namespace Core.Domains
{
    public class BankTransaction
    {
        public Guid Id { get; set; }
        
        public Guid SourceBankAccountId { get; set; }

        public Guid TargetBankAccountId { get; set; }

        public float Amount { get; set; }

        public long Created { get; set; }

        public long LastModified { get; set; }

        public required string Type { get; set; }

        public string? Description { get; set; }

        public required string Status { get; set; }
    }

    public enum TransactionStatus
    {
        Pending,
        Approved,
        Rejected
    }
}