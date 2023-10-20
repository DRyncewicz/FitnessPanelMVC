using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IRecipeProductRepository<T>
    {
        public int CreateRecipeProduct(T recipeProduct);
        public void DeleteRecipeProduct(int id);
        public IQueryable<T> GetRecipeProductsByRecipeId(int recipeId);
        public int UpdateRecipeProduct(T recipeProduct);
    }
}
