using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonalExpenseTrackerWeb.Models;

namespace PersonalExpenseTrackerWeb.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionsDBContext _context;

        public TransactionController(TransactionsDBContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var transactionsDBContext = _context.Transactions.Include(t => t.Category);
            return View(await transactionsDBContext.ToListAsync());
        }

        // GET: Transaction/CreateOrEdit
        public IActionResult CreateOrEdit(int id=0)
        {
            PopulateCategories();
            if (id == 0)
            {
                return View(new TransactionModel());
            }
            else
            {
                return View(_context.Transactions.Find(id));
            }
        }

        // POST: Transaction/CreateOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("TransactionId,CategoryId,Amount,Note,Date")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                if (transactionModel.TransactionId == 0)
                {
                    _context.Add(transactionModel);
                }
                else
                {
                    _context.Update(transactionModel);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            PopulateCategories();
            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _context.Transactions.FindAsync(id);
            if (transactionModel != null)
            {
                _context.Transactions.Remove(transactionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Categories.ToList();
            CategoryModel DefaultCategory = new CategoryModel() { CategoryId = 0, Title = "Choose a Category" };
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
    }
}
