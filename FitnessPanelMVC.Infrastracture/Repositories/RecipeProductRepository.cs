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
    public class RecipeProductRepository : IRecipeProductRepository
    {
        private readonly DbContext _dbCotenxt;

        public RecipeProductRepository(DbContext dbContext)
        {
            _dbCotenxt = dbContext;
        }

        public async Task<int> CreateAsync(RecipeProduct recipeProduct)
        {
            await _dbCotenxt.RecipeProduct.AddAsync(recipeProduct);
            await _dbCotenxt.SaveChangesAsync();
            return recipeProduct.Id;
        }

        public async Task DeleteAsync(int productId, int recipeId)
        {
            var recipeProduct = await _dbCotenxt.RecipeProduct.FirstAsync(rp => rp.ProductId == productId &&
            rp.RecipeId == recipeId);
            if (recipeProduct != null)
            {
                _dbCotenxt.RecipeProduct.Remove(recipeProduct);
                await _dbCotenxt.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateAsnyc(RecipeProduct recipeProduct)
        {
            if (await _dbCotenxt.RecipeProduct.FindAsync(recipeProduct.Id) != null)
            {
                _dbCotenxt.RecipeProduct.Update(recipeProduct);
                await _dbCotenxt.SaveChangesAsync();
                return recipeProduct.Id;
            }
            return 0;
        }

        public IQueryable<RecipeProduct> GetAll()
        {
            var recipeProducts = _dbCotenxt.RecipeProduct;
            return recipeProducts.AsQueryable(); ;
        }
    }
}
