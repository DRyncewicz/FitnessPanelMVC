using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IRecipeProductRepository
    {
        Task<int> CreateAsync(RecipeProduct recipeProduct);

        Task DeleteAsync(int productId, int recipeId);

        IQueryable<RecipeProduct> GetAll();

        Task<int> UpdateAsnyc(RecipeProduct recipeProduct);
    }
}
