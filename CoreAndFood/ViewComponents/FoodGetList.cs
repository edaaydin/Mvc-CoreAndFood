using CoreAndFood.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.ViewComponents
{
    public class FoodGetList : ViewComponent
    {
        private readonly FoodRepository _foodRepo;

        public FoodGetList(FoodRepository foodRepo)
        {
            _foodRepo = foodRepo;
        }

        public IViewComponentResult Invoke()
        {
            var foodList = _foodRepo.TList();
            return View(foodList);
        }
    }
}
