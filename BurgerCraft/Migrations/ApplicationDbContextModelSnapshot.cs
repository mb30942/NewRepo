﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BurgerCraft.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BurgerCraft.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BurgerCraft.Models.Burger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BurgerTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("BurgerTypeId");

                    b.ToTable("Burgers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BurgerTypeId = 1,
                            Description = "Fresh veggie patty with lettuce and tomato",
                            ImagePath = "/images/veggie-delight.jpg",
                            Name = "Veggie Delight",
                            Price = 5.99m
                        },
                        new
                        {
                            Id = 2,
                            BurgerTypeId = 2,
                            Description = "Grilled chicken with mayo and lettuce",
                            ImagePath = "/images/chicken-supreme.jpg",
                            Name = "Chicken Supreme",
                            Price = 6.99m
                        },
                        new
                        {
                            Id = 3,
                            BurgerTypeId = 3,
                            Description = "Juicy beef patty with cheddar cheese",
                            ImagePath = "/images/classic-beef.jpg",
                            Name = "Classic Beef",
                            Price = 7.99m
                        });
                });

            modelBuilder.Entity("BurgerCraft.Models.BurgerIngredient", b =>
                {
                    b.Property<int>("BurgerId")
                        .HasColumnType("integer");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("BurgerId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("burgerIngredients");

                    b.HasData(
                        new
                        {
                            BurgerId = 1,
                            IngredientId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            BurgerId = 1,
                            IngredientId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            BurgerId = 2,
                            IngredientId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            BurgerId = 2,
                            IngredientId = 3,
                            Quantity = 1
                        },
                        new
                        {
                            BurgerId = 3,
                            IngredientId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            BurgerId = 3,
                            IngredientId = 4,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("BurgerCraft.Models.BurgerType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("BurgerTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Veggie"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Chicken"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Beef"
                        });
                });

            modelBuilder.Entity("BurgerCraft.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Lettuce",
                            Price = 0.50m
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tomato",
                            Price = 0.75m
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cheddar Cheese",
                            Price = 1.50m
                        },
                        new
                        {
                            Id = 4,
                            Name = "Beef Patty",
                            Price = 3.00m
                        },
                        new
                        {
                            Id = 5,
                            Name = "Chicken Patty",
                            Price = 2.50m
                        },
                        new
                        {
                            Id = 6,
                            Name = "Bacon",
                            Price = 2.00m
                        },
                        new
                        {
                            Id = 7,
                            Name = "Egg",
                            Price = 1.20m
                        },
                        new
                        {
                            Id = 8,
                            Name = "Ham",
                            Price = 2.50m
                        },
                        new
                        {
                            Id = 9,
                            Name = "Turkey Patty",
                            Price = 3.20m
                        },
                        new
                        {
                            Id = 10,
                            Name = "Swiss Cheese",
                            Price = 1.70m
                        },
                        new
                        {
                            Id = 11,
                            Name = "Blue Cheese",
                            Price = 1.80m
                        },
                        new
                        {
                            Id = 12,
                            Name = "Fried Onion Rings",
                            Price = 1.50m
                        },
                        new
                        {
                            Id = 13,
                            Name = "BBQ Sauce",
                            Price = 0.40m
                        },
                        new
                        {
                            Id = 14,
                            Name = "Honey Mustard",
                            Price = 0.50m
                        },
                        new
                        {
                            Id = 15,
                            Name = "Vegan Patty",
                            Price = 3.50m
                        },
                        new
                        {
                            Id = 16,
                            Name = "Vegan Cheese",
                            Price = 1.75m
                        },
                        new
                        {
                            Id = 17,
                            Name = "Avocado",
                            Price = 2.00m
                        },
                        new
                        {
                            Id = 18,
                            Name = "Spinach",
                            Price = 0.70m
                        },
                        new
                        {
                            Id = 19,
                            Name = "Grilled Zucchini",
                            Price = 1.20m
                        },
                        new
                        {
                            Id = 20,
                            Name = "Hummus",
                            Price = 0.90m
                        },
                        new
                        {
                            Id = 21,
                            Name = "Mushrooms",
                            Price = 1.00m
                        },
                        new
                        {
                            Id = 22,
                            Name = "Roasted Peppers",
                            Price = 1.30m
                        },
                        new
                        {
                            Id = 23,
                            Name = "Vegan Mayo",
                            Price = 0.60m
                        },
                        new
                        {
                            Id = 24,
                            Name = "Cucumber Slices",
                            Price = 0.50m
                        },
                        new
                        {
                            Id = 25,
                            Name = "Olives",
                            Price = 1.10m
                        });
                });

            modelBuilder.Entity("BurgerCraft.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BurgerId")
                        .HasColumnType("integer");

                    b.Property<List<int>>("IngredientIds")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BurgerCraft.Models.Burger", b =>
                {
                    b.HasOne("BurgerCraft.Models.BurgerType", "BurgerType")
                        .WithMany("Burgers")
                        .HasForeignKey("BurgerTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BurgerType");
                });

            modelBuilder.Entity("BurgerCraft.Models.BurgerIngredient", b =>
                {
                    b.HasOne("BurgerCraft.Models.Burger", "Burger")
                        .WithMany("BurgerIngredients")
                        .HasForeignKey("BurgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BurgerCraft.Models.Ingredient", "Ingredient")
                        .WithMany("BurgerIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Burger");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BurgerCraft.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BurgerCraft.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BurgerCraft.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BurgerCraft.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BurgerCraft.Models.Burger", b =>
                {
                    b.Navigation("BurgerIngredients");
                });

            modelBuilder.Entity("BurgerCraft.Models.BurgerType", b =>
                {
                    b.Navigation("Burgers");
                });

            modelBuilder.Entity("BurgerCraft.Models.Ingredient", b =>
                {
                    b.Navigation("BurgerIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
