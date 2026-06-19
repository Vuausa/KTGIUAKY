using Microsoft.EntityFrameworkCore;
using KTGIUAKY.Models;

namespace KTGIUAKY.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DishCategory_BCS240047> DishCategories_BCS240047 { get; set; }
        public DbSet<Dish_BCS240047> Dishes_BCS240047 { get; set; }
        public DbSet<DishImage_BCS240047> DishImages_BCS240047 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Dish_BCS240047>()
                .HasIndex(d => new { d.Name, d.DishCategoryId })
                .IsUnique();

            modelBuilder.Entity<DishCategory_BCS240047>()
                .HasMany(dc => dc.Dishes)
                .WithOne(d => d.DishCategory)
                .HasForeignKey(d => d.DishCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Dish_BCS240047>()
                .HasMany(d => d.DishImages)
                .WithOne(di => di.Dish)
                .HasForeignKey(di => di.DishId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DishCategory_BCS240047>().HasData(
                new DishCategory_BCS240047 { Id = 1, Name = "Món khai vị", Description = "Các món ăn nhẹ khai vị" },
                new DishCategory_BCS240047 { Id = 2, Name = "Món chính", Description = "Các món ăn chính" },
                new DishCategory_BCS240047 { Id = 3, Name = "Món tráng miệng", Description = "Các món tráng miệng ngọt" }
            );

            modelBuilder.Entity<Dish_BCS240047>().HasData(
                new Dish_BCS240047 { Id = 1, Name = "Salad rau trộn", Price = 35000m, PreparationTime = 10, IsAvailable = true, Description = "Salad rau tươi trộn dầu giấm", DishCategoryId = 1 },
                new Dish_BCS240047 { Id = 2, Name = "Súp cua", Price = 45000m, PreparationTime = 15, IsAvailable = true, Description = "Súp cua thơm ngon", DishCategoryId = 1 },
                new Dish_BCS240047 { Id = 3, Name = "Bò nướng lá lốt", Price = 120000m, PreparationTime = 25, IsAvailable = true, Description = "Bò nướng lá lốt thơm lừng", DishCategoryId = 2 },
                new Dish_BCS240047 { Id = 4, Name = "Cơm chiên hải sản", Price = 85000m, PreparationTime = 20, IsAvailable = true, Description = "Cơm chiên với tôm, mực", DishCategoryId = 2 },
                new Dish_BCS240047 { Id = 5, Name = "Kem dâu", Price = 25000m, PreparationTime = 5, IsAvailable = false, Description = "Kem dâu tươi mát lạnh", DishCategoryId = 3 }
            );

            modelBuilder.Entity<DishImage_BCS240047>().HasData(
                new DishImage_BCS240047 { Id = 1, ImageUrl = "/images/salad.jpg", IsThumbnail = true, DishId = 1 },
                new DishImage_BCS240047 { Id = 2, ImageUrl = "/images/supcua.jpg", IsThumbnail = true, DishId = 2 },
                new DishImage_BCS240047 { Id = 3, ImageUrl = "/images/bonuong.jpg", IsThumbnail = true, DishId = 3 },
                new DishImage_BCS240047 { Id = 4, ImageUrl = "/images/comchien.jpg", IsThumbnail = true, DishId = 4 },
                new DishImage_BCS240047 { Id = 5, ImageUrl = "/images/kemdau.jpg", IsThumbnail = true, DishId = 5 }
            );
        }
    }
}
