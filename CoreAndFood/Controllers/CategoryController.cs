using CoreAndFood.Models.Entities;
using CoreAndFood.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repo; // Dependency Injection yaparak Category Nesnesi icin calısıyor!

        public CategoryController(CategoryRepository repo)
        {
            _repo = repo;
        }

        //[Authorize] // category listemi yetkisi olanlar gormez!
        public IActionResult Index(string p)
        {
            if (!string.IsNullOrEmpty(p))
            {
                return View(_repo.List(x => x.CategoryName == p));
            }

            return View(_repo.TList());
        }

        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category category) // Valid = Geçerli
        {
            if (!ModelState.IsValid) // modelim geçerli değilse CategoryAdd sayfasına yonlendir.
            {

                return View("CategoryAdd");
            }

            _repo.TAdd(category);

            return RedirectToAction("Index", "Category");
        }

        public IActionResult CategoryGet(int id) // id gonderdigimi getir
        {
            var x = _repo.TGet(id);
            Category category = new Category()
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName, // x = dısarıdan gonderdigim name oluyor!
                CategoryDescription = x.CategoryDescription
            };
            return View(category);
        }

        [HttpPost]
        public IActionResult CategoryUpdate(Category category)
        {
            var x = _repo.TGet(category.CategoryId);

            x.CategoryName = category.CategoryName;
            x.CategoryDescription = category.CategoryDescription;
            x.Status = true;

            _repo.TUpdate(x);

            return RedirectToAction("Index");
        }

        // id'sini gonderdigim nesnenin delete butonuna basınca statusunu false yap!
        public IActionResult CategoryDelete(int id)
        {
            var x = _repo.TGet(id); // gondermis oldugum x nesnenin id'sini bul!
            x.Status = false; // gonderilen x nesnesinin statusunu false cek!
            _repo.TUpdate(x); // gonderilen x nesnesinin statusunu false yaptıktan sonra db'de update et!

            return RedirectToAction("Index");
        }
    }
}
