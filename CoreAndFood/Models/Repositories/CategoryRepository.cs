using CoreAndFood.Models.Context;
using CoreAndFood.Models.Entities;
using CoreAndFood.Repositories;

namespace CoreAndFood.Models.Repositories
{
    public class CategoryRepository : GenericRepository<Category> // category icin calıs!
    {
        public CategoryRepository(CoreAndFoodDbContext context) : base(context)
        {
        }
    }
}
