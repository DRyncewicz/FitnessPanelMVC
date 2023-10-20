using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IExerciseRepository<T>
    {
        public int CreateExercise(T exercise);
        public void DeleteExercise(int id);
        public IQueryable<T> GetExercises();
        public IQueryable<T> GetExercisesByNameContains(string name);
    }
}
