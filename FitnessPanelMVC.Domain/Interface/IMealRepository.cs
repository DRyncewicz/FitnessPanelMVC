using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IMealRepository<T>
    {
        public int CreateMeal(T meal);
        public int UpdateMeal(T meal); 
        public void DeleteMeal(int id);
        public IQueryable<Meal> GetMealsByDate(DateTime date);
    }
}
