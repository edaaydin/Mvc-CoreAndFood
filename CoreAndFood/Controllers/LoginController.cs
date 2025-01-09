using CoreAndFood.Models.Context;
using CoreAndFood.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreAndFood.Controllers
{
    public class LoginController : Controller
    {
        private readonly CoreAndFoodDbContext _context;


        public LoginController(CoreAndFoodDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous] // Bu action authorize'den muaf !
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(Admin admin)
        {
            var dataValue = _context.Admins
                .FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);

            if (dataValue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.UserName) // type,value
                };

                var userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Category"); // login başarılı ise buraya don !
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login"); //çıkış yapıldıgında buraya don !
        }
    }
}
