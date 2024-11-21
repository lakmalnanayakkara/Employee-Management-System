using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entity
{
    public class Employee
    {
        [Key]
        [Column("customer_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; }

        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [MaxLength(50)]
        [Column("position")]
        public string Position { get; set; }

        [DataType(DataType.Currency)]
        [Column("salary", TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }
    }
}
