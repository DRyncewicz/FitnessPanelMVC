using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IMealRepository
    {
        public int CreateMeal(Meal meal);
        public int UpdateMeal(Meal meal); 
        public void DeleteMeal(int id);
        public IQueryable<Meal> GetAllMeals();
    }
}
