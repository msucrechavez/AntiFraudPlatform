using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionManagerAPI.Dtos
{
    public class BankTransactionRequest
    {
        public Guid SourceBankAccountId { get; set; }

        public Guid TargetBankAccountId { get; set; }

        public float Amount { get; set; }

        public string? Description { get; set; }
    }
}