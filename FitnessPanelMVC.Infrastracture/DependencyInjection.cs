using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IExerciseRepository, ExerciseRepository>();
            services.AddTransient<IMealProductRepository, MealProductRepository>();
            services.AddTransient<IMealRepository, MealRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IRecipeProductRepository, RecipeProductRepository>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();
            services.AddTransient<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
            services.AddTransient<IWorkoutRepository, WorkoutRepository>();
            return services;
        }
    }
}
