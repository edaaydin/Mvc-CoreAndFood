using CoreAndFood.Models.Context;
using CoreAndFood.Models.Entities;
using CoreAndFood.Repositories;

namespace CoreAndFood.Models.Repositories
{
    public class FoodRepository : GenericRepository<Food> // Food nesnesi icin calıs !
    {
        public FoodRepository(CoreAndFoodDbContext context) : base(context)
        {
        }
    }
}
