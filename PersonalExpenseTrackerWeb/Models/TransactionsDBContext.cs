using Microsoft.EntityFrameworkCore;

namespace PersonalExpenseTrackerWeb.Models
{
    public class TransactionsDBContext:DbContext
    {
        public TransactionsDBContext(DbContextOptions options):base(options)
        { }

        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
    }
}
