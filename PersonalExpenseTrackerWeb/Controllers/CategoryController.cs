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
    public class CategoryController : Controller
    {
        private readonly TransactionsDBContext _context;

        public CategoryController(TransactionsDBContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Category/CreateOrEdit
        public IActionResult CreateOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new CategoryModel());
            } 
            else
            {
                return View(_context.Categories.Find(id));
            }
        }

        // POST: Category/CreateOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("CategoryId,Title,Icon,Type")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                if (categoryModel.CategoryId == 0) {
                    _context.Add(categoryModel);
                }
                else
                {
                    _context.Update(categoryModel);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryModel = await _context.Categories.FindAsync(id);
            if (categoryModel != null)
            {
                _context.Categories.Remove(categoryModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
