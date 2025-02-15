using AntiFraud.Core.Domains;
using AntiFraud.Core.Repositories;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AntiFraud.Infrastructure.Repositories_Impl
{
    public class BankAccountDailyAmountCacheRepository : IBankAccountDailyAmountCacheRepository
    {
        private readonly IDatabase _database;

        public BankAccountDailyAmountCacheRepository(IConnectionMultiplexer muxer)
        {
            _database = muxer.GetDatabase();
        }

        public async Task Create(BankAccountDailyAmount bankAccountDailyAmount)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DateTimeOffset endOfDay = now.Date.AddDays(1);
            TimeSpan expiry = endOfDay - now;
            string key = bankAccountDailyAmount.BankAccountId.ToString();
            string jsonData = JsonConvert.SerializeObject(bankAccountDailyAmount);
            await _database.StringSetAsync(key, jsonData, expiry);
        }

        public async Task<BankAccountDailyAmount?> GetById(Guid bankAccountId)
        {
            string key = bankAccountId.ToString();
            string? jsonData = await _database.StringGetAsync(key);
            return string.IsNullOrEmpty(jsonData) ? null : JsonConvert.DeserializeObject<BankAccountDailyAmount>(jsonData);
        }

        public async Task Update(BankAccountDailyAmount bankAccountDailyAmount)
        {
            string key = bankAccountDailyAmount.BankAccountId.ToString();
            string jsonData = JsonConvert.SerializeObject(bankAccountDailyAmount);
            await _database.StringSetAsync(key, jsonData, expiry: TimeSpan.FromHours(1));
        }
    }
}
