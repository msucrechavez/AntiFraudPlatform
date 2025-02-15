namespace TransactionManager.Core.Services
{
    public interface IBankTransactionEventConsumerService
    {
        public Task ProcessBankTransactionEvent(CancellationToken cancellationToken);
    }
}
