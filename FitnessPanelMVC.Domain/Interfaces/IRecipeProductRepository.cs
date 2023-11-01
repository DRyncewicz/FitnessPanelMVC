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
        public int Create(RecipeProduct recipeProduct);

        public void Delete(int productId, int recipeId);

        public IQueryable<RecipeProduct> GetAll();

        public int Update(RecipeProduct recipeProduct);
    }
}
