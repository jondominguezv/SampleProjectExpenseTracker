using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalExpenseTrackerWeb.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Category title is required.")]
        public string Title { get; set; } = "";

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Icon is required. See https://getemoji.com/ for examples.")]
        public string Icon { get; set; } = "";

        [Column(TypeName = "nvarchar(50)")]
        public string Type { get; set; } = "Expense";

        [NotMapped]
        public string? TitleWithIcon
        {
            get
            {
                return $"{this.Icon} {this.Title}";
            }
        }
    }
}
