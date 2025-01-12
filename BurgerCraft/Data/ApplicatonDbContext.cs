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
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
     .UseNpgsql("Host=localhost;Port=5432;Database=burgerCraft;Username=postgres;Password=tijana")
     .ConfigureWarnings(warnings => warnings.Default(WarningBehavior.Ignore));

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
    }


}