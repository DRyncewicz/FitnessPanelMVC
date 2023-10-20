using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IMealProductRepository<T>
    {
        public int CreateMealProduct(T mealProduct);
        public void DeleteMealProduct(int id);
        public IQueryable<T> GetMealProductByMealId(int mealId);
        public int UpdateMealProduct(T mealProduct);
    }
}
