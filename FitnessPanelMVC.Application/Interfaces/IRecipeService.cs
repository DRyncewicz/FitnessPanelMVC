using FitnessPanelMVC.Application.ViewModels.Product;
using FitnessPanelMVC.Application.ViewModels.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IRecipeService
    {
        int AddNewRecipe(NewRecipeVm newRecipeVm, string userId);
        int AddProductToRecipe(int productId, int recipeId, double weight);
        void DeleteRecipe(int recipeId);
        ListRecipeForListVm GetRecipesForList(int pageSize, int pageNo, string searchString, string userId);
        NewProductVm UpdateProductOnRecipeChange(int recipeId);
    }
}
