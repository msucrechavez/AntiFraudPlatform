namespace Core.Domains
{
    public class BankAccount
    {
        public Guid Id { get; set; }

        public required string DocumentIdentifier { get; set; }

        public float Balance { get; set; }

        public long Created { get; set; }
    }
}