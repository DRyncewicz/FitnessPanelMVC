using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IMealProductRepository
    {
        public int CreateMealProduct(MealProduct mealProduct);
        public void DeleteMealProduct(int id);
        public IQueryable<MealProduct> GetMealProductByMealId(int mealId);
        public int UpdateMealProduct(MealProduct mealProduct);
    }
}
