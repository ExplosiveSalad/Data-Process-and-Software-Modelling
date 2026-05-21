using Microsoft.EntityFrameworkCore;
using PATBMS_Web.Data;
using PATBMS_Web.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data source=patbms.db"));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();
app.UseSession();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Seed database with initial data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    DbSeeder.Seed(context);

    // Set up BedManager Singleton with wards from database
    var bedManager = BedManager.GetInstance();
    var wards = context.Wards.ToList();
    foreach (var ward in wards)
    {
        bedManager.AddWard(ward);
    }
}

app.Run();
