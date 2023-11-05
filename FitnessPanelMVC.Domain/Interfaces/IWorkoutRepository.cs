using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IWorkoutRepository
    {
        Task<int> CreateAsync(Workout workout);

        Task DeleteAsync(int id);

        public IQueryable<Workout> GetAll();
        Task<Workout> GetByIdAsync(int id);
        Task<int> UpdateAsync(Workout workout);
    }
}
