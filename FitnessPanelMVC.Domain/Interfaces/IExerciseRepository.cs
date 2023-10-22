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
        public int CreateExercise(Exercise exercise);
        public void DeleteExercise(int id);
        public IQueryable<Exercise> GetExercises();
        public IQueryable<Exercise> GetExercisesByNameContains(string name);
    }
}
