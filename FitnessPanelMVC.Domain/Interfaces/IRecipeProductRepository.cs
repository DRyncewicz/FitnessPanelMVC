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
        public int CreateRecipeProduct(RecipeProduct recipeProduct);
        public void DeleteRecipeProduct(int id);
        public IQueryable<RecipeProduct> GetAllRecipeProducts();
        public int UpdateRecipeProduct(RecipeProduct recipeProduct);
    }
}
