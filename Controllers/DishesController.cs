using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KTGIUAKY.Data;
using KTGIUAKY.Models;

namespace KTGIUAKY.Controllers
{
    public class DishesController : Controller
    {
        private readonly AppDbContext _context;

        public DishesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int? categoryId, bool? isAvailable, decimal? minPrice, decimal? maxPrice, string sortOrder)
        {
            var query = _context.Dishes_BCS240047
                .Include(d => d.DishCategory)
                .Include(d => d.DishImages)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(d => d.Name.Contains(searchString));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(d => d.DishCategoryId == categoryId.Value);
            }

            if (isAvailable.HasValue)
            {
                query = query.Where(d => d.IsAvailable == isAvailable.Value);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(d => d.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(d => d.Price <= maxPrice.Value);
            }

            if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
            {
                ViewBag.PriceError = "Khoảng giá không hợp lệ. Giá thấp phải nhỏ hơn hoặc bằng giá cao.";
            }

            query = sortOrder switch
            {
                "price_asc" => query.OrderBy(d => d.Price),
                "price_desc" => query.OrderByDescending(d => d.Price),
                "time_asc" => query.OrderBy(d => d.PreparationTime),
                _ => query.OrderBy(d => d.Id)
            };

            var dishes = await query.ToListAsync();

            ViewBag.SearchString = searchString;
            ViewBag.CategoryId = categoryId;
            ViewBag.IsAvailable = isAvailable;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.SortOrder = sortOrder;
            ViewBag.Categories = new SelectList(await _context.DishCategories_BCS240047.ToListAsync(), "Id", "Name", categoryId);

            if (!dishes.Any() && (!string.IsNullOrEmpty(searchString) || categoryId.HasValue || isAvailable.HasValue || minPrice.HasValue || maxPrice.HasValue))
            {
                ViewBag.NoResults = "Không tìm thấy món ăn phù hợp.";
            }

            return View(dishes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes_BCS240047
                .Include(d => d.DishCategory)
                .Include(d => d.DishImages)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.DishCategories_BCS240047, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dish_BCS240047 dish)
        {
            ViewBag.Categories = new SelectList(_context.DishCategories_BCS240047, "Id", "Name", dish.DishCategoryId);

            if (string.IsNullOrWhiteSpace(dish.Name))
            {
                ModelState.AddModelError("Name", "Tên món ăn không được để trống.");
                return View(dish);
            }

            if (dish.Price <= 0)
            {
                ModelState.AddModelError("Price", "Giá phải lớn hơn 0.");
                return View(dish);
            }

            if (dish.PreparationTime <= 0)
            {
                ModelState.AddModelError("PreparationTime", "Thời gian chế biến phải lớn hơn 0.");
                return View(dish);
            }

            var categoryExists = await _context.DishCategories_BCS240047.AnyAsync(c => c.Id == dish.DishCategoryId);
            if (!categoryExists)
            {
                ModelState.AddModelError("DishCategoryId", "Loại món ăn không tồn tại.");
                return View(dish);
            }

            var exists = await _context.Dishes_BCS240047
                .AnyAsync(d => d.Name == dish.Name && d.DishCategoryId == dish.DishCategoryId);
            if (exists)
            {
                ModelState.AddModelError("Name", "Tên món ăn không được trùng trong cùng một loại.");
                return View(dish);
            }

            _context.Add(dish);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Thêm món ăn thành công!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes_BCS240047.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_context.DishCategories_BCS240047, "Id", "Name", dish.DishCategoryId);
            return View(dish);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dish_BCS240047 dish)
        {
            if (id != dish.Id)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_context.DishCategories_BCS240047, "Id", "Name", dish.DishCategoryId);

            if (string.IsNullOrWhiteSpace(dish.Name))
            {
                ModelState.AddModelError("Name", "Tên món ăn không được để trống.");
                return View(dish);
            }

            if (dish.Price <= 0)
            {
                ModelState.AddModelError("Price", "Giá phải lớn hơn 0.");
                return View(dish);
            }

            if (dish.PreparationTime <= 0)
            {
                ModelState.AddModelError("PreparationTime", "Thời gian chế biến phải lớn hơn 0.");
                return View(dish);
            }

            var categoryExists = await _context.DishCategories_BCS240047.AnyAsync(c => c.Id == dish.DishCategoryId);
            if (!categoryExists)
            {
                ModelState.AddModelError("DishCategoryId", "Loại món ăn không tồn tại.");
                return View(dish);
            }

            var exists = await _context.Dishes_BCS240047
                .AnyAsync(d => d.Name == dish.Name && d.DishCategoryId == dish.DishCategoryId && d.Id != dish.Id);
            if (exists)
            {
                ModelState.AddModelError("Name", "Tên món ăn không được trùng trong cùng một loại.");
                return View(dish);
            }

            try
            {
                _context.Update(dish);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(dish.Id))
                {
                    return NotFound();
                }
                throw;
            }
            TempData["Success"] = "Cập nhật món ăn thành công!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes_BCS240047
                .Include(d => d.DishCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes_BCS240047.FindAsync(id);
            if (dish != null)
            {
                _context.Dishes_BCS240047.Remove(dish);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes_BCS240047.Any(e => e.Id == id);
        }
    }
}
