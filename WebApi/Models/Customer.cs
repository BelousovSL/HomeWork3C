using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public long Id { get; init; }
        
        [Required]
        [Column("first_name")]
        public string Firstname { get; init; }

        [Required]
        [Column("last_name")]
        public string Lastname { get; init; }
    }
}