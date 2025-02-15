using Core.Domains;

namespace Core.Repositories
{
    public interface IBankAccountRepository
    {
        public Task<BankAccount> Create(BankAccount bankAccount);

        public Task<List<BankAccount>> GetAll();

        public Task<BankAccount> GetById(Guid id);

        public Task Update(BankAccount bankAccount);
    }
}