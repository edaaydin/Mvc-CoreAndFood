using CoreAndFood.Models.Context;
using CoreAndFood.Models.Entities;
using CoreAndFood.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList.Extensions;

namespace CoreAndFood.Controllers
{
    public class FoodController : Controller
    {

        private readonly FoodRepository _repo; // Dependency Injection yaparak Food Nesnesi icin calısıyor!
        private readonly CoreAndFoodDbContext _context;

        public FoodController(FoodRepository repo, CoreAndFoodDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        // ToPagedList(1, 7) Metodu = başlangıç değeri(kaçıncı ayfadan başlar?), sayfada kaç adet deger olacağını ister.

        public IActionResult Index(int page = 1) // food listeleme
        {
            return View(_repo.TList("Category").ToPagedList(page, 7));
        }

        // gorunum tarafındaki işlemler için kullanırım
        public IActionResult FoodAdd()
        {
            List<SelectListItem> values = (
                from x in _context.Categories.ToList()
                select new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.CategoryId.ToString()
                }).ToList();
            ViewBag.value = values;


            //FillSelectlist();
            // todo burayı sor!

            //var category = _context.Categories.Select(x => new SelectListItem
            //{
            //    Text = x.CategoryName,
            //    Value = x.CategoryId.ToString()
            //}).ToList();

            return View();
        }

        private List<SelectListItem> FillSelectlist()
        {
            return _context.Categories.Select(a => new SelectListItem() { Text = a.CategoryName, Value = a.CategoryId.ToString() }).ToList();
        }

        [HttpPost]
        public IActionResult FoodAdd(ProductsAdd pAdd)
        {
            Food food = new Food();

            if (pAdd.ImageUrl != null)
            {
                var extension = Path.GetExtension(pAdd.ImageUrl.FileName);

                var newImageName = Guid.NewGuid() + extension;

                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resimler/", newImageName);

                var stream = new FileStream(location, FileMode.Create);

                pAdd.ImageUrl.CopyTo(stream);

                food.ImageUrl = newImageName;
            }

            food.FoodName = pAdd.FoodName;
            food.Price = pAdd.Price;
            food.Stock = pAdd.Stock;
            food.CategoryId = pAdd.CategoryId;
            // food.Description = pAdd.Description;

            _repo.TAdd(food);
            return RedirectToAction("Index");
        }

        public IActionResult FoodDelete(int id)
        {
            var delete = _context.Foods.Find(id);
            _repo.TDelete(delete);
            return RedirectToAction("Index");
        }

        public IActionResult FoodGet(int id)
        {
            var x = _repo.TGet(id);

            List<SelectListItem> values = (
                from y in _context.Categories.ToList()
                select new SelectListItem
                {
                    Text = y.CategoryName,
                    Value = y.CategoryId.ToString()
                }).ToList();
            ViewBag.value = values;

            Food food = new Food() // atamalara ait degerler gelir.
            {
                FoodId = x.FoodId,
                CategoryId = x.CategoryId,
                FoodName = x.FoodName,
                Price = x.Price,
                Stock = x.Stock,
                Description = x.Description,
                ImageUrl = x.ImageUrl

            };
            return View(food); // food'a gondermis oldugumuz degerleri tutar.
        }

        [HttpPost]
        public IActionResult FoodUpdate(Food food)
        {
            var x = _repo.TGet(food.FoodId);
            x.FoodName = x.FoodName;
            x.Price = food.Price;
            x.Stock = food.Stock;
            x.Price = food.Price;
            x.ImageUrl = food.ImageUrl;
            x.Description = food.Description;
            x.CategoryId = food.CategoryId;

            _repo.TUpdate(x);
            return RedirectToAction("Index");
        }
        public IActionResult FoodDetail(int id)
        {
            var value = _context.Foods.Find(id);
            return View(value);
        }
    }
}