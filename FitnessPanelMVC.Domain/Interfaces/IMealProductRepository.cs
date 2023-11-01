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
        public void Create(MealProduct mealProduct);

        public void Delete(int mealId, int productId);

        public IQueryable<MealProduct> GetAll();

        public void Update(MealProduct mealProduct);
    }
}
