using FitnessPanelMVC.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure
{
    public class DbContext : IdentityDbContext
    {
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealProduct> MealProduct { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeProduct> RecipeProduct { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercise { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<BodyIndicator> BodyIndicators { get; set; }


        public DbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MealProduct>()
                .HasKey(it => new {it.MealId, it.ProductId });

            builder.Entity<MealProduct>()
                .HasOne<Meal>(it => it.Meal)
                .WithMany(i => i.MealProducts)
                .HasForeignKey(it => it.MealId);

            builder.Entity<MealProduct>()
                .HasOne<Product>(it => it.Product)
                .WithMany(i => i.MealProducts)
                .HasForeignKey(it => it.ProductId);

            builder.Entity<RecipeProduct>()
                .HasKey(it => new { it.RecipeId, it.ProductId });

            builder.Entity<RecipeProduct>()
                .HasOne<Recipe>(it => it.Recipe)
                .WithMany(i => i.RecipeProducts)
                .HasForeignKey(it => it.RecipeId);

            builder.Entity<RecipeProduct>()
                .HasOne<Product>(it => it.Product)
                .WithMany(i => i.RecipeProduct)
                .HasForeignKey(IT => IT.ProductId);

            builder.Entity<WorkoutExercise>()
                .HasKey(it => new { it.WorkoutId, it.ExerciseId });

            builder.Entity<WorkoutExercise>()
                .HasOne<Workout>(it => it.Workout)
                .WithMany(i => i.WorkoutExercises)
                .HasForeignKey(it => it.WorkoutId);

            builder.Entity<WorkoutExercise>()
                .HasOne<Exercise>(it => it.Exercise)
                .WithMany(i => i.WorkoutExercises)
                .HasForeignKey(IT => IT.ExerciseId);

            builder.Entity<Recipe>()
                .HasOne<IdentityUser>(it => it.User)
                .WithMany()
                .HasForeignKey(it => it.UserId);

            builder.Entity<Meal>()
                .HasOne<IdentityUser>(it => it.User)
                .WithMany()
                .HasForeignKey(it => it.UserId);

            builder.Entity<Product>()
                .HasOne<IdentityUser>(it => it.User)
                .WithMany()
                .HasForeignKey(it => it.UserId)
                .IsRequired(false);

            builder.Entity<Workout>()
                .HasOne<IdentityUser>(it => it.User)
                .WithMany()
                .HasForeignKey(it => it.UserId);

        }
    }
}
