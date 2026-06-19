using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTGIUAKY.Models
{
    [Table("DishImages_BCS240047")]
    public class DishImage_BCS240047
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Đường dẫn ảnh không được để trống")]
        [Display(Name = "Đường dẫn ảnh")]
        public string ImageUrl { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public bool IsThumbnail { get; set; }

        [Required]
        public int DishId { get; set; }

        [ForeignKey("DishId")]
        public Dish_BCS240047 Dish { get; set; }
    }
}
