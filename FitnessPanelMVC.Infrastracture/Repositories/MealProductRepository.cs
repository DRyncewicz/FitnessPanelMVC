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
    public class MealProductRepository : IMealProductRepository
    {
        private readonly DbContext _dbContext;

        public MealProductRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(MealProduct mealProduct)
        {
            await _dbContext.MealProduct.AddAsync(mealProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId, int mealId)
        {
            var mealProduct = await _dbContext.MealProduct
                .FirstOrDefaultAsync(mp => mp.MealId == mealId && mp.ProductId == productId);
            if (mealProduct != null)
            {
                _dbContext.MealProduct.Remove(mealProduct);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(MealProduct mealProduct)
        {
            var existingMealProduct = await _dbContext.MealProduct
                .FindAsync(mealProduct.MealId, mealProduct.ProductId);

            if (existingMealProduct != null)
            {
                _dbContext.Entry(existingMealProduct).CurrentValues.SetValues(mealProduct);
                await _dbContext.SaveChangesAsync();
            }
        }

        public IQueryable<MealProduct> GetAll()
        {
            return _dbContext.MealProduct;
        }
    }
}
