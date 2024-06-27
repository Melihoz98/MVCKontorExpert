using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCKontorExpert.BusinessLogic;
using MVCKontorExpert.Data;
using MVCKontorExpert.DataAccess;
using MVCKontorExpert.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                         ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<ICategoryData, CategoryDataLogic>();
builder.Services.AddScoped<ICategoryAccess, CategoryAccess>();
builder.Services.AddScoped<IProductData, ProductDataLogic>();
builder.Services.AddScoped<IProductAccess, ProductAccess>();
builder.Services.AddScoped<IParentCategoryData, ParentCategoryDataLogic>();
builder.Services.AddScoped<IParentCategoryAccess, ParentCategoryAccess>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
