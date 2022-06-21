
using Microsoft.EntityFrameworkCore;

using Recipe.Service.Domain.Models;


namespace Recipe.Service.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Domain.Models.Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Instruction> Instructions { get; set; }
    public DbSet<RecipeIngredients> RecipeIngredients { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Models.Recipe>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Image).HasMaxLength(128000).IsRequired(false);
            entity.Property(x => x.Description).HasMaxLength(500);
            entity.HasMany(x => x.Ingredients).WithOne().HasForeignKey(x => x.Id);
            entity.HasMany(x => x.Instructions).WithOne(x => x.Recipe).IsRequired(false);
        });

        modelBuilder.Entity<Instruction>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Description).HasMaxLength(500).IsRequired();
            entity.Property(x => x.Order).HasDefaultValue(false).IsRequired();
            entity.HasOne(x => x.Recipe).WithMany(x => x.Instructions).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Name).HasMaxLength(50);

            entity.HasMany(x => x.Recipes).WithOne().HasForeignKey(x => x.IngredientId);
        });

        modelBuilder.Entity<RecipeIngredients>(entity =>
        {
            entity.HasOne(x => x.Recipe).WithMany(x => x.Ingredients).HasForeignKey(x => x.RecipeId);
            entity.HasOne(x => x.Ingredient).WithMany(x => x.Recipes).HasForeignKey(x => x.IngredientId);
            entity.HasIndex(x => new { x.RecipeId, x.IngredientId }).IsUnique();

            entity.Property(x => x.Quantity).HasMaxLength(25);
            entity.Property(x => x.Calories);
        });

        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, Name = "Cherries" },
            new Ingredient { Id = 2, Name = "Flour" },
            new Ingredient { Id = 3, Name = "Egg" },
            new Ingredient { Id = 4, Name = "Butter" },
            new Ingredient { Id = 5, Name = "Water" },
            new Ingredient { Id = 6, Name = "Yeast" },
            new Ingredient { Id = 7, Name = "Sugar" },
            new Ingredient { Id = 8, Name = "Apples" }
        );

        modelBuilder.Entity<Domain.Models.Recipe>().HasData(
            new Domain.Models.Recipe { Id = 1, Name = "Cherry Pie", Description = "A whole made Cherry Pie.", Image = "https://tastesbetterfromscratch.com/wp-content/uploads/2020/04/Cherry-Pie-11.jpg" }
        );
        modelBuilder.Entity<Instruction>().HasData(
            new Instruction { Id = 1, Description = "Preheat over to 375", Order = 1, RecipeId= 1 }
        );
        modelBuilder.Entity<RecipeIngredients>().HasData(
            new RecipeIngredients { Id = 1, IngredientId = 1, Quantity = "2 cups", Calories = 500, RecipeId = 1 },
            new RecipeIngredients { Id = 2, IngredientId = 2, Quantity = "2 cups", Calories = 200, RecipeId = 1 },
            new RecipeIngredients { Id = 3, IngredientId = 3, Quantity = "3", Calories = 150, RecipeId = 1 },
            new RecipeIngredients { Id = 4, IngredientId = 4, Quantity = "1 stick", Calories = 1000, RecipeId = 1 },
            new RecipeIngredients { Id = 5, IngredientId = 5, Quantity = "1 cup", Calories = 0, RecipeId = 1 },
            new RecipeIngredients { Id = 6, IngredientId = 6, Quantity = "1 Tbs", Calories = 0, RecipeId = 1 },
            new RecipeIngredients { Id = 7, IngredientId = 7, Quantity = "1 cup", Calories = 375, RecipeId = 1 }
        );
    }
}
