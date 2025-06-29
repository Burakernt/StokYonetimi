using System;
using Microsoft.EntityFrameworkCore;
using StokYonetimiNew.Data;
using StokYonetimiNew.Filters;
using StokYonetimiNew.Models;

var builder = WebApplication.CreateBuilder(args);

// MVC + API
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<RequireLoginAttribute>();
});
builder.Services.AddControllers(); // API endpoint'leri için
builder.Services.AddHttpContextAccessor();
// Eðer Session kullanýyorsanýz, bunlarý da eklemeyi unutmayýn:
 builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
     options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// EF Core - PostgreSQL
builder.Services.AddDbContext<StokContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session ayarlarý
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();






// Hata sayfalarý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Session ve Authorization
app.UseSession();
app.UseAuthorization();

// Endpoint tanýmlarý
app.MapControllers(); // API
app.MapControllerRoute(
    name: "default",
 pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
