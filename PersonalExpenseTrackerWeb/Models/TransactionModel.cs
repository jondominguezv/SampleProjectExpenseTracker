using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalExpenseTrackerWeb.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }

        public decimal Amount { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}
