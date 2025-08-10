using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTrackerWeb.Models;
using System.Globalization;
using System.Threading.Tasks;

namespace PersonalExpenseTrackerWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly TransactionsDBContext _context;

        public DashboardController(TransactionsDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Last 7 Days of Transactions
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<TransactionModel> TransactionList = await _context.Transactions
                .Include(x => x.Category)
                .ToListAsync();

            List<TransactionModel> IncomeTransactionList = TransactionList
                .Where(t => t.Category.Type == "Income")
                .ToList();
            List<TransactionModel> ExpenseTransactionList = TransactionList
                .Where(t => t.Category.Type == "Expense")
                .ToList();
            List<TransactionModel> SaveTransactionList = TransactionList
                .Where(t => t.Category.Type == "Save")
                .ToList();

            decimal TotalIncome = IncomeTransactionList.Sum(t => t.Amount);
            ViewBag.TotalIncome = TotalIncome.ToString("C");

            decimal TotalExpense = ExpenseTransactionList.Sum(t => t.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C");

            decimal TotalSave = SaveTransactionList.Sum(t => t.Amount);
            ViewBag.TotalSave = TotalSave.ToString("C");

            decimal RemainingBalance =  TotalIncome - TotalExpense - TotalSave;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.RemainingBalance = String.Format(culture, "{0:C}", RemainingBalance);

            // Doughnut Chart - Expense by Category
            ViewBag.DoughnutChartData = TransactionList
                .Where(i => i.Category.Type == "Expense" || i.Category.Type == "Save")
                .GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.TitleWithIcon,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("C"),
                })
                .ToList();

            // Recent Transactions
            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i =>  i.Category)
                .OrderByDescending(j => j.Date)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}
