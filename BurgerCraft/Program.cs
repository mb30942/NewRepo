using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BurgerCraft.Models;
using BurgerCraft.Repositories.Implementations;
using BurgerCraft.Repositories.Interfaces;
var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder()
 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
 .Build();

// Register the ApplicationDbContext with PostgreSQL.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
 options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IBurgerRepository, BurgerRepository>();
builder.Services.AddScoped<IBurgerTypeRepository, BurgerTypeRepository>();


// Add Identity services, specifying ApplicationUser and IdentityRole.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();
// Configure the HTTP request pipeline.


if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.Run();