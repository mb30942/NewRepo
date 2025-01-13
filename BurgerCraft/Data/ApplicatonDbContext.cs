using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BurgerCraft.Models;
using Microsoft.AspNetCore.Identity;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<BurgerType> BurgerTypes { get; set; }
    public DbSet<Burger> Burgers { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<BurgerIngredient> burgerIngredients { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
     .UseNpgsql("Host=localhost;Port=5432;Database=burgerCraft;Username=postgres;Password=tijana")
     .ConfigureWarnings(warnings => warnings.Default(WarningBehavior.Ignore));

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Configure relationship between burger and ingredients
        modelBuilder.Entity<BurgerIngredient>()
                .HasKey(bi => new { bi.BurgerId, bi.IngredientId });

        modelBuilder.Entity<BurgerIngredient>()
               .HasOne(bi => bi.Burger)
               .WithMany(b => b.BurgerIngredients)
               .HasForeignKey(bi => bi.BurgerId)
               .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BurgerIngredient>()
            .HasOne(bi => bi.Ingredient)
            .WithMany(i => i.BurgerIngredients)
            .HasForeignKey(bi => bi.IngredientId);

        // Configure the Burger → BurgerType relationship
        modelBuilder.Entity<Burger>()
            .HasOne(b => b.BurgerType)
            .WithMany(bt => bt.Burgers)
            .HasForeignKey(b => b.BurgerTypeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Optional: Seed initial data
        modelBuilder.Entity<BurgerType>().HasData(
            new BurgerType { Id = 1, Name = "Veggie" },
            new BurgerType { Id = 2, Name = "Chicken" },
            new BurgerType { Id = 3, Name = "Beef" }
        );

        modelBuilder.Entity<Burger>().HasData(
            new Burger { Id = 1, Name = "Veggie Delight", Price = 5.99M, Description = "Fresh veggie patty with lettuce and tomato", BurgerTypeId = 1,ImagePath = "/images/veggie-delight.jpg" },
            new Burger { Id = 2, Name = "Chicken Supreme", Price = 6.99M, Description = "Grilled chicken with mayo and lettuce", BurgerTypeId = 2, ImagePath = "/images/chicken-supreme.jpg" },
            new Burger { Id = 3, Name = "Classic Beef", Price = 7.99M, Description = "Juicy beef patty with cheddar cheese", BurgerTypeId = 3, ImagePath = "/images/classic-beef.jpg" }
        );
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, Name = "Lettuce", Price = 0.50M },
            new Ingredient { Id = 2, Name = "Tomato", Price = 0.75M },
            new Ingredient { Id = 3, Name = "Cheddar Cheese", Price = 1.50M },
            new Ingredient { Id = 4, Name = "Beef Patty", Price = 3.00M },
            new Ingredient { Id = 5, Name = "Chicken Patty", Price = 2.50M },
            new Ingredient { Id = 6, Name = "Bacon", Price = 2.00M },
            new Ingredient { Id = 7, Name = "Egg", Price = 1.20M },
            new Ingredient { Id = 8, Name = "Ham", Price = 2.50M },
            new Ingredient { Id = 9, Name = "Turkey Patty", Price = 3.20M },
            new Ingredient { Id = 10, Name = "Swiss Cheese", Price = 1.70M },
            new Ingredient { Id = 11, Name = "Blue Cheese", Price = 1.80M },
            new Ingredient { Id = 12, Name = "Fried Onion Rings", Price = 1.50M },
            new Ingredient { Id = 13, Name = "BBQ Sauce", Price = 0.40M },
            new Ingredient { Id = 14, Name = "Honey Mustard", Price = 0.50M },
            new Ingredient { Id = 15, Name = "Vegan Patty", Price = 3.50M },
            new Ingredient { Id = 16, Name = "Vegan Cheese", Price = 1.75M },
            new Ingredient { Id = 17, Name = "Avocado", Price = 2.00M },
            new Ingredient { Id = 18, Name = "Spinach", Price = 0.70M },
            new Ingredient { Id = 19, Name = "Grilled Zucchini", Price = 1.20M },
            new Ingredient { Id = 20, Name = "Hummus", Price = 0.90M },
            new Ingredient { Id = 21, Name = "Mushrooms", Price = 1.00M },
            new Ingredient { Id = 22, Name = "Roasted Peppers", Price = 1.30M },
            new Ingredient { Id = 23, Name = "Vegan Mayo", Price = 0.60M },
            new Ingredient { Id = 24, Name = "Cucumber Slices", Price = 0.50M },
            new Ingredient { Id = 25, Name = "Olives", Price = 1.10M }
        );
    }


}