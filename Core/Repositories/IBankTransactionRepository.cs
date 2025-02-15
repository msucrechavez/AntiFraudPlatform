using Core.Domains;

namespace Core.Repositories
{
    public interface IBankTransactionRepository
    {
        public Task<BankTransaction> Create(BankTransaction bankTransaction);

        public Task<List<BankTransaction>> GetByBankAccountId(Guid bankAccountId);

        public Task<BankTransaction> GetById(Guid id);

        public Task Update(BankTransaction bankTransaction);
    }
}
