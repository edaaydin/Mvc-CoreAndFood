using CoreAndFood.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.ViewComponents
{
    public class CategoryGetList : ViewComponent
    {
        private readonly CategoryRepository _categoryRepo;

        public CategoryGetList(CategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IViewComponentResult Invoke()
        {
            var categoryList = _categoryRepo.TList();

            return View(categoryList);
        }
    }
}
