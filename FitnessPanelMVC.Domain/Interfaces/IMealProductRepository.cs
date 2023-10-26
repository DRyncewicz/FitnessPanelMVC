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
        public void CreateMealProduct(MealProduct mealProduct);
        public void DeleteMealProduct(int mealId, int productId);
        public IQueryable<MealProduct> GetAllMealProducts();
        public void UpdateMealProduct(MealProduct mealProduct);
    }
}
