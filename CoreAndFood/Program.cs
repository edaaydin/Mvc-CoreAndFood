using CoreAndFood.Models.Context;
using CoreAndFood.Models.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CoreAndFoodDbContext>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<FoodRepository>();

// claim
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Login/Index"; // Giris yapmak icin yonlendirme


    });

// Bu authorize i�lemi tum sayfalarda uygulans�n !

// RequireAuthenticatedUser = sisteme yetkili kullanc� gerekli

builder.Services.AddMvc(
     config =>
     {
         var policy = new AuthorizationPolicyBuilder()
         .RequireAuthenticatedUser()
         .Build();

         config.Filters.Add(new AuthorizeFilter(policy));
     });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
