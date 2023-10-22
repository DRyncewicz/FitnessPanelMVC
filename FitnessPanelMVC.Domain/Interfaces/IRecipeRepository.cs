using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IRecipeRepository
    {
        public int CreateRecipe(Recipe recipe);
        public IQueryable<Recipe> GetAllRecipes();
        public void RemoveRecipe(int id);
        public int UpdateRecipe(Recipe recipe);
    }
}
