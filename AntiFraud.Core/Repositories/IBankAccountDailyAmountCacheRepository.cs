using AntiFraud.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Core.Repositories
{
    public interface IBankAccountDailyAmountCacheRepository
    {
        public Task Create(BankAccountDailyAmount bankAccountDailyAmount);

        public Task Update(BankAccountDailyAmount bankAccountDailyAmount);

        public Task<BankAccountDailyAmount?> GetById(Guid bankAccountId);
    }
}