using AntiFraud.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Core.Events
{
    public class BankTransactionEvent
    {
        public required string Type { get; set; }

        public BankTransactionSummary BankTransactionSummary { get; set; }

        public bool IsValid { get; set; }
    }
}