using CoreAndFood.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.ViewComponents
{
    public class FoodListByCategory : ViewComponent
    {
        private readonly FoodRepository _foodRepo;

        public FoodListByCategory(FoodRepository foodRepo)
        {
            _foodRepo = foodRepo;
        }

        public IViewComponentResult Invoke(int id) // id yi classta gormicem
        {

            var foodList = _foodRepo.List(x => x.CategoryId == id); // id'ye gore urun listesini almak istiyorum!


            return View(foodList);
        }
    }
}
