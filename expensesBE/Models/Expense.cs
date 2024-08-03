using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expensesBE.Models
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }

        public IdentityUser? User { get; set; }
    }
}
