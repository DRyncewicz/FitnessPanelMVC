using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    internal class RecipeRepository : IRecipeRepository
    {
        private readonly Context _dbContext;
        public RecipeRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateRecipe(Recipe recipe)
        {
            _dbContext.Recipes.Add(recipe);
            _dbContext.SaveChanges();
            return recipe.Id;
        }

        public void RemoveRecipe(int id)
        {
            var recipe = _dbContext.Recipes.Find(id);
            if (recipe != null)
            {
                _dbContext.Recipes.Remove(recipe);
                _dbContext.SaveChanges();
            }
        }

        public int UpdateRecipe(Recipe recipe)
        {
            if(_dbContext.Recipes.Find(recipe.Id) != null)
            {
                _dbContext.Recipes.Update(recipe);
                _dbContext.SaveChanges();
                return recipe.Id;
            }
            return 0;
        }

        public IQueryable<Recipe> GetAllRecipes()
        {
            var recipes = _dbContext.Recipes
                .Include(m => m.RecipeProducts)
                .ThenInclude(m => m.Product);
            return recipes.AsQueryable();
        }
    }
}
