using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BurgerCraft.Models;
var builder = WebApplication.CreateBuilder(args);
// Register the ApplicationDbContext with PostgreSQL.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); // Using Npgsql for PostgreSQL
// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Identity services, specifying ApplicationUser and IdentityRole.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();