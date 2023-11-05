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
        Task CreateAsync(MealProduct mealProduct);

        Task DeleteAsync(int mealId, int productId);

        IQueryable<MealProduct> GetAll();

        Task UpdateAsync(MealProduct mealProduct);
    }
}
