using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IExerciseRepository
    {
        public Task<int> CreateAsync(Exercise exercise);

        public Task Delete(int id);

        public IQueryable<Exercise> GetAll();
        Task<Exercise> GetByIdAsync(int id);
    }
}
