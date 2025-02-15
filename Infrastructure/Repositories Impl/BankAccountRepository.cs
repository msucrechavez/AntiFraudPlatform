
using AutoMapper;
using Core.Domains;
using Core.Repositories;
using Infrastructure.Config;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository_Impl
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public BankAccountRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BankAccount> Create(BankAccount bankAccount)
        {
            var bankAccountEntity = _mapper.Map<BankAccountEntity>(bankAccount);
            _context.BankAccounts.Add(bankAccountEntity);
            await _context.SaveChangesAsync();
            bankAccount.Id = bankAccountEntity.Id;
            return bankAccount;
        }

        public async Task<List<BankAccount>> GetAll()
        {
            var bankAccountEntityList = await _context.BankAccounts.ToListAsync();
            return _mapper.Map<List<BankAccount>>(bankAccountEntityList);

        }

        public async Task<BankAccount> GetById(Guid id)
        {
            var bankAccountEntity = await _context.BankAccounts.FindAsync(id);
            return _mapper.Map<BankAccount>(bankAccountEntity);
        }

        public Task Update(BankAccount bankAccount)
        {
            _mapper.Map<BankAccountEntity>(bankAccount);
            return _context.SaveChangesAsync();
        }
    }
}
