using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using FitnessPanelMVC.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly DbContext _dbContext;

        public MealRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Meal meal)
        {
            await _dbContext.Meals.AddAsync(meal);
            await _dbContext.SaveChangesAsync();
            return meal.Id;

        }

        public async Task DeleteAsync(int id)
        {
            var meal = await _dbContext.Meals.FindAsync(id);
            if (meal != null)
            {
                _dbContext.Meals.Remove(meal);
                await _dbContext.SaveChangesAsync();
            }
        }

        public IQueryable<Meal> GetAll()
        {
            var meals = _dbContext.Meals.Include(m => m.MealProducts)
                .ThenInclude(mp => mp.Product);
            return meals;
        }

        public async Task<int> UpdateAsync(Meal meal)
        {
            if (await _dbContext.Meals.FindAsync(meal.Id) != null)
            {
                _dbContext.Meals.Update(meal);
                await _dbContext.SaveChangesAsync();
                return meal.Id;
            }
            return 0;
        }

        public async Task<Meal> GetByIdAsync(int id)
        {
            var meal = await _dbContext.Meals.FirstOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                meal = new Meal();
            }
            return meal;
        }
    }
}
