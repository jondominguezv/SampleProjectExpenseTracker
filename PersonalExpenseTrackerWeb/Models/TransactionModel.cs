using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalExpenseTrackerWeb.Models
{
    public class TransactionModel
    {
        [Key]
        public int TransactionId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Please select a transaction category.")]
        public int CategoryId { get; set; }
        public CategoryModel? Category { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? Note { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        [NotMapped]
        public string? CategoryTitleWithIcon
        {
            get
            {
                return Category == null ? "" : Category.TitleWithIcon;
            }
        }

        [NotMapped]
        public string? FormattedAmount
        {
            get
            {
                if (Category != null)
                {
                    return Category.Type == "Income" ? "+" + Amount.ToString("C") : "-" + Amount.ToString("C");
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
