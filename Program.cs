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
    .AddRoles<IdentityRole>()
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

// Ensure the roles are created
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Customer", "Test" };
    foreach (var role in roles)
    {
        if (!roleManager.RoleExistsAsync(role).Result)
        {
            var result = roleManager.CreateAsync(new IdentityRole(role)).Result;
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create role {role}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }
    }
}

// Ensure the admin user is created
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string email = "admin@admin.com";
    string password = "Test1234,";
    var user = userManager.FindByEmailAsync(email).Result;
    if (user == null)
    {
        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = "Admin",
            LastName = "User",
            CompanyName = "Admin Company",
            CVR = "Admin CVR",
            Address = "Admin Address",
            City = "Admin City",
            Zip = "12345",
            Country = "Admin Country",
            Phone = "1234567890"
        };

        var result = userManager.CreateAsync(user, password).Result;
        if (result.Succeeded)
        {
            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            if (!roleResult.Succeeded)
            {
                throw new Exception($"Failed to assign role to user: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
            }
        }
        else
        {
            throw new Exception($"Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
}

app.Run();
