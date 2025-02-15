using Core.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    [Table("BankTransaction")]
    public class BankTransactionEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid SourceBankAccountId { get; set; }

        [ForeignKey("SourceBankAccountId")]
        public BankAccountEntity SourceBankAccount { get; set; }

        public Guid TargetBankAccountId { get; set; }

        [ForeignKey("TargetBankAccountId")]
        public BankAccountEntity TargetSourceBankAccount { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public long Created { get; set; }

        [Required]
        public long LastModified { get; set; }

        [Required]
        public required string Type { get; set; }

        public string? Description { get; set; }

        [Required]
        public required string Status { get; set; }
    }
}
