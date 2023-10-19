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
        private readonly Context _dbCotenxt;

        public MealRepository(Context dbContext)
        {
            _dbCotenxt = dbContext;
        }
        public int CreateMeal(Meal meal)
        {
            _dbCotenxt.Meals.Add(meal);
            _dbCotenxt.SaveChanges();
            return meal.Id;

        }

        public void DeleteMeal(int mealId)
        {
            var meal = _dbCotenxt.Meals.Find(mealId);
            if (meal != null)
            {
                _dbCotenxt.Meals.Remove(meal);
                _dbCotenxt.SaveChanges();
            }
        }

        public IQueryable<Meal> GetMealsByDate(DateTime date)
        {
            var meals = _dbCotenxt.Meals.Where(i => i.MealDate == date);
            return meals;
        }

        public int UpdateMeal(int mealId)
        {
            var meal = _dbCotenxt.Meals.Find(mealId);
            if (meal != null)
            {
                _dbCotenxt.Meals.Update(meal);
                _dbCotenxt.SaveChanges();
                return meal.Id;
            }
            return 0;
        }
    }
}
