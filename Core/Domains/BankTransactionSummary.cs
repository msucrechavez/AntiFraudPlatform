using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManager.Core.Domains
{
    public class BankTransactionSummary
    {
        public Guid Id { get; set; }

        public Guid SourceBankAccountId { get; set; }

        public float Amount { get; set; }

    }
}