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
        Task<int> AddNewAsync(NewRecipeVm newRecipeVm, string userId);

        Task<int> AddProductToRecipeAsync(int productId, int recipeId, double weight);

        Task DeleteProductFromMealByIdsAsync(int productId, int recipeId);

        Task DeleteAsync(int recipeId);

        Task<RecipeForListVm> GetDetailsByIdAsync(int recipeId);

        Task<ListRecipeForListVm> GetForListAsync(int pageSize, int pageNo, string searchString, string userId);
        Task<NewProductVm> UpdateProductOnRecipeChangeAsync(int recipeId);
    }
}
