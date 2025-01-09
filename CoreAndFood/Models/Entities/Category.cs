using System.ComponentModel.DataAnnotations;

namespace CoreAndFood.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name Not Empty !")]
        [StringLength(20, ErrorMessage = "Please Only Enter 5-20 characters", MinimumLength = 5)]
        public string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public bool Status { get; set; } // Genelde arka plandaki durumunu gormek icin ekleriz. Kullanıcının gormesine gerek yok!

        // navigation prop
        public List<Food>? Foods { get; set; } // Bir kategorinin birden fazla yiyeceği olur.

    }
}
