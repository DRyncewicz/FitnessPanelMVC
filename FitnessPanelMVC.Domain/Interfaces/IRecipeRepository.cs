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
        Task<int> CreateAsync(Recipe recipe);

        public IQueryable<Recipe> GetAll();

        Task DeleteAsync(int id);

        Task<int> UpdateAsync(Recipe recipe);
        Task<Recipe> GetByIdAsync(int id);
    }
}
