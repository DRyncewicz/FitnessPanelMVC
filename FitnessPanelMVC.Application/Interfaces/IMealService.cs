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
        int AddNewMeal(NewMealVm newMealVm);
        int AddProductToMeal(int productId, int mealId, double weight);
        void DeleteMealById(int mealId);
        void DeleteProductFromMealById(int productId, int mealId);
        MealForListVm GetMealDetailsById(int mealId);
        ListMealForListVm GetMealsForListByDate(DateTime date);
    }
}
