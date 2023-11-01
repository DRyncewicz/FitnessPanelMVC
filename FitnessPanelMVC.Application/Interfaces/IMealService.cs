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
        int AddNew(NewMealVm newMealVm, string userId);

        int AddProductToMeal(int productId, int mealId, double weight);

        void DeleteById(int mealId);

        void DeleteProductFromMealById(int productId, int mealId);

        MealForListVm GetDetailsById(int mealId);

        ListMealForListVm GetForListByDate(DateTime date, string userId);
    }
}
