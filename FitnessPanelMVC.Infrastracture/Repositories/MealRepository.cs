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
    public class MealRepository : IMealRepository<Meal>
    {
        private readonly Context _dbContext;

        public MealRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }
        public int CreateMeal(Meal meal)
        {
            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();
            return meal.Id;

        }

        public void DeleteMeal(int id)
        {
            var meal = _dbContext.Meals.Find(id);
            if (meal != null)
            {
                _dbContext.Meals.Remove(meal);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Meal> GetMealsByDate(DateTime date)
        {
            var meals = _dbContext.Meals.Where(i => i.MealDate == date);
            return meals;
        }

        public int UpdateMeal(int id)
        {
            var meal = _dbContext.Meals.Find(id);
            if (meal != null)
            {
                _dbContext.Meals.Update(meal);
                _dbContext.SaveChanges();
                return meal.Id;
            }
            return 0;
        }
    }
}
