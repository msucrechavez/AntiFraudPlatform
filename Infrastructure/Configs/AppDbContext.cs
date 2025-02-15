using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Config
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<BankAccountEntity> BankAccounts { get; set; }

        public DbSet<BankTransactionEntity> BankTransactions { get; set; }
    }
}