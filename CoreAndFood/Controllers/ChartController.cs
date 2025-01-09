using CoreAndFood.Models.Context;
using CoreAndFood.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.Controllers
{
    public class ChartController : Controller
    {
        private readonly CoreAndFoodDbContext _context;

        public ChartController(CoreAndFoodDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Index3()
        {
            return View();
        }

        public IActionResult VisualizeProductResult2()
        {
            return Json(FoodList());

        }

        public List<Class2> FoodList()
        {
            List<Class2> cs2 = new List<Class2>();

            using (var c = new CoreAndFoodDbContext())
            {
                cs2 = c.Foods.Select(x => new Class2
                {
                    Food = x.FoodName,
                    Quantity = x.Stock,
                }).ToList();
            }
            return cs2;
        }

        public IActionResult VisualizeProductResult()
        {
            return Json(ProList());

        }

        public List<Class1> ProList() // statik liste
        {
            List<Class1> cs = new List<Class1>();

            cs.Add(new Class1()
            {
                Name = "Computer",
                Quantity = 150
            });

            cs.Add(new Class1()
            {
                Name = "Lcd",
                Quantity = 75
            });

            cs.Add(new Class1()
            {
                Name = "Usb Disk",
                Quantity = 220
            });
            return cs;
        }

        public IActionResult Statistics()
        {
            var deger1 = _context.Foods.Count(); // Total Food
            ViewBag.d1 = deger1;

            var deger2 = _context.Categories.Count(); // Total Category
            ViewBag.d2 = deger2;

            var foodId = _context.Categories.Where(x => x.CategoryName == "Fruit").Select(y => y.CategoryId).FirstOrDefault(); // categori tablasunun categoryname = fruit olanların id'sini getirsin.

            var deger3 = _context.Foods.Where(x => x.CategoryId == foodId).Count(); // Fruit Count
            ViewBag.d3 = deger3;

            var deger4 = _context.Foods.Where(x => x.CategoryId == 2).Count(); // Vegetable count
            ViewBag.d4 = deger4;

            var deger5 = _context.Foods.Sum(x => x.Stock); // Sum Food
            ViewBag.d5 = deger5;


            var deger6 = _context.Foods.Where(x => x.CategoryId == _context.Categories.Where(y => y.CategoryName == "Breakfast").Select(z => z.CategoryId).FirstOrDefault()).Count(); // Breakfast count
            ViewBag.d6 = deger6;


            var deger7 = _context.Foods.OrderByDescending(x => x.Stock).Select(y => y.FoodName).FirstOrDefault(); // bana ilk sırada olanın degerini getir.(z-a) = sondan ilk olanı getir.
            ViewBag.d7 = deger7;


            var deger8 = _context.Foods.OrderBy(x => x.Stock).Select(y => y.FoodName).FirstOrDefault(); // bana ilk sırada olanın degerini getir. (a-z)
            ViewBag.d8 = deger8;


            var deger9 = _context.Foods.Average(x => x.Price);
            ViewBag.d9 = deger9;


            var deger10 = _context.Categories.Where(x => x.CategoryName == "Fruit").Select(y => y.CategoryId).FirstOrDefault();
            var deger10p = _context.Foods.Where(y => y.CategoryId == deger10).Sum(x => x.Stock);
            ViewBag.d10 = deger10p; // Toplam meyve sayısı


            var deger11 = _context.Categories.Where(x => x.CategoryName == "Vegetables").Select(y => y.CategoryId).FirstOrDefault();
            var deger11p = _context.Foods.Where(y => y.CategoryId == deger11).Sum(x => x.Stock);
            ViewBag.d11 = deger11p; // Toplam meyve sayısı



            var deger12 = _context.Foods.OrderByDescending(x => x.Price).Select(y => y.FoodName).FirstOrDefault(); // bana ilk sırada olanın degerini getir.(z-a) = sondan ilk olanı getir.
            ViewBag.d12 = deger12;

            return View();
        }
    }
}
