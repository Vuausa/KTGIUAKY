using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KTGIUAKY.Models
{
    [Table("DishCategories_BCS240047")]
    public class DishCategory_BCS240047
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên loại món ăn không được để trống")]
        public string Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Dish_BCS240047> Dishes { get; set; } = new List<Dish_BCS240047>();
    }
}
