using AutoMapper;
using Core.Domains;
using Core.Repositories;
using Infrastructure.Config;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories_Impl
{
    public class BankTransactionRepository : IBankTransactionRepository
    {

        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public BankTransactionRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BankTransaction> Create(BankTransaction bankTransaction)
        {
            var bankTransactionEntity = _mapper.Map<BankTransactionEntity>(bankTransaction);
            _context.BankTransactions.Add(bankTransactionEntity);
            await _context.SaveChangesAsync();
            bankTransaction.Id = bankTransactionEntity.Id;
            return bankTransaction;
        }


        public async Task<List<BankTransaction>> GetByBankAccountId(Guid bankAccountId)
        {
            var bankTransactionEntityList = await _context.BankTransactions.Where(txn => txn.SourceBankAccountId == bankAccountId).ToListAsync();
            return _mapper.Map<List<BankTransaction>>(bankTransactionEntityList);
        }

        public async Task<BankTransaction> GetById(Guid id)
        {
            var bankTransactionEntity = await _context.BankTransactions.FindAsync(id);
            return _mapper.Map<BankTransaction>(bankTransactionEntity);
        }

        public async Task Update(BankTransaction bankTransaction)
        {
            var existingEntity = await _context.BankTransactions.FindAsync(bankTransaction.Id);
            _mapper.Map(bankTransaction, existingEntity);
            await _context.SaveChangesAsync();
        }
    }
}
