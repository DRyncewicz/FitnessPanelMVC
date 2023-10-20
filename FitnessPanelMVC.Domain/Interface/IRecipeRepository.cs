using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IRecipeRepository<T>
    {
        public int CreateRecipe(T recipe);
        public IQueryable<T> GetAllRecipes();
        public void RemoveRecipe(int id);
        public int UpdateRecipe(T recipe);
    }
}
