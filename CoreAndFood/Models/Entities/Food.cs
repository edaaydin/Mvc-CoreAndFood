namespace CoreAndFood.Models.Entities
{
    public class Food
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? ImageUrl { get; set; }
        public int Stock { get; set; }

        // navigation prop

        public int CategoryId { get; set; } // fk
        public virtual Category Category { get; set; } // bir yiyeceğin bir tane kategorisi olur

    }
}
