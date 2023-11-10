using FitnessPanelMVC.Application.ViewModels.Meal;
using FitnessPanelMVC.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IMealService
    {
        Task<int> AddNewAsync(NewMealVm newMealVm, string userId);

        Task<int> AddProductToMealAsync(int productId, int mealId, double weight);

        Task DeleteByIdAsync(int mealId);

        Task DeleteProductFromMealByIdAsync(int productId, int mealId);

        Task<MealForListVm> GetDetailsByIdAsync(int mealId);

        Task<ListMealForListVm> GetForListByDate(DateTime date, string userId);
    }
}
