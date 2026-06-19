using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTGIUAKY.Models
{
    [Table("Dishes_BCS240047")]
    public class Dish_BCS240047
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên món ăn không được để trống")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Giá")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Thời gian chế biến là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Thời gian chế biến phải lớn hơn 0")]
        [Display(Name = "Thời gian chế biến (phút)")]
        public int PreparationTime { get; set; }

        [Display(Name = "Đang bán")]
        public bool IsAvailable { get; set; } = true;

        public string? Description { get; set; }

        [Required]
        [Display(Name = "Loại món ăn")]
        public int DishCategoryId { get; set; }

        [ForeignKey("DishCategoryId")]
        [Display(Name = "Loại món ăn")]
        public DishCategory_BCS240047 DishCategory { get; set; }

        public ICollection<DishImage_BCS240047> DishImages { get; set; } = new List<DishImage_BCS240047>();
    }
}
