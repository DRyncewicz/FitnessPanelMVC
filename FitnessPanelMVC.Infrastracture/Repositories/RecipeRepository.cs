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
        private readonly DbContext _dbContext;

        public RecipeRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Recipe recipe)
        {
            await _dbContext.Recipes.AddAsync(recipe);
            await _dbContext.SaveChangesAsync();
            return recipe.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var recipe = await _dbContext.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _dbContext.Recipes.Remove(recipe);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateAsync(Recipe recipe)
        {
            if(await _dbContext.Recipes.FindAsync(recipe.Id) != null)
            {
                _dbContext.Recipes.Update(recipe);
                await _dbContext.SaveChangesAsync();
                return recipe.Id;
            }
            return 0;
        }

        public IQueryable<Recipe> GetAll()
        {
            var recipes = _dbContext.Recipes
                .Include(m => m.RecipeProducts)
                .ThenInclude(m => m.Product);
            return recipes.AsQueryable();
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            var recipe = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            if (recipe == null)
            {
                recipe = new Recipe();
            }
            return recipe;
        }
    }
}
