using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTGIUAKY.Data;
using KTGIUAKY.Models;

namespace KTGIUAKY.Controllers
{
    public class DishCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public DishCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DishCategories_BCS240047.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.DishCategories_BCS240047
                .Include(c => c.Dishes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] DishCategory_BCS240047 category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.DishCategories_BCS240047.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] DishCategory_BCS240047 category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DishCategories_BCS240047.Any(e => e.Id == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.DishCategories_BCS240047
                .Include(c => c.Dishes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) return NotFound();

            if (category.Dishes.Any())
            {
                TempData["Error"] = "Không thể xóa loại món ăn đang có món ăn sử dụng.";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.DishCategories_BCS240047
                .Include(c => c.Dishes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            if (category.Dishes.Any())
            {
                TempData["Error"] = "Không thể xóa loại món ăn đang có món ăn sử dụng.";
                return RedirectToAction(nameof(Index));
            }

            _context.DishCategories_BCS240047.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
