using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IMealRepository
    {
        Task<int> CreateAsync(Meal meal);

        Task<int> UpdateAsync(Meal meal);

        Task DeleteAsync(int id);

        public IQueryable<Meal> GetAll();
        Task<Meal> GetByIdAsync(int id);
    }
}
