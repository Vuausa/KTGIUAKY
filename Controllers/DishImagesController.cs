using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTGIUAKY.Data;
using KTGIUAKY.Models;

namespace KTGIUAKY.Controllers
{
    public class DishImagesController : Controller
    {
        private readonly AppDbContext _context;

        public DishImagesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DishImage_BCS240047 image)
        {
            var dish = await _context.Dishes_BCS240047.FindAsync(image.DishId);
            if (dish == null)
            {
                TempData["Error"] = "Món ăn không tồn tại.";
                return RedirectToAction("Details", "Dishes", new { id = image.DishId });
            }

            if (string.IsNullOrWhiteSpace(image.ImageUrl))
            {
                TempData["Error"] = "Đường dẫn ảnh không được để trống.";
                return RedirectToAction("Details", "Dishes", new { id = image.DishId });
            }

            if (image.IsThumbnail)
            {
                var currentThumbnails = await _context.DishImages_BCS240047
                    .Where(di => di.DishId == image.DishId && di.IsThumbnail)
                    .ToListAsync();
                foreach (var thumb in currentThumbnails)
                {
                    thumb.IsThumbnail = false;
                }
            }

            _context.Add(image);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Dishes", new { id = image.DishId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetThumbnail(int id)
        {
            var image = await _context.DishImages_BCS240047.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            var currentThumbnails = await _context.DishImages_BCS240047
                .Where(di => di.DishId == image.DishId && di.IsThumbnail)
                .ToListAsync();
            foreach (var thumb in currentThumbnails)
            {
                thumb.IsThumbnail = false;
            }

            image.IsThumbnail = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Dishes", new { id = image.DishId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _context.DishImages_BCS240047.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            int dishId = image.DishId;
            _context.DishImages_BCS240047.Remove(image);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Dishes", new { id = dishId });
        }
    }
}
