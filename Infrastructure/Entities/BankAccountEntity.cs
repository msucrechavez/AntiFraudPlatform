using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Infrastructure.Entities
{
    [Table("BankAccount")]
    public class BankAccountEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string DocumentIdentifier { get; set; }

        [Required]
        public float Balance { get; set; }

        [Required]
        public long Created { get; set; }
    }
}
