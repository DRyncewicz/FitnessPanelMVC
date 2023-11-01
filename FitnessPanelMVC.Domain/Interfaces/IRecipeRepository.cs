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
        public int Create(Recipe recipe);

        public IQueryable<Recipe> GetAll();

        public void Remove(int id);

        public int Update(Recipe recipe);
    }
}
