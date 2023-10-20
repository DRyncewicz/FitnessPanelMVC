using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IWorkoutExerciseRepository<T>
    {
        public int CreateWorkoutExercise(T workoutExercise);
        public void DeleteWorkoutExercise(int id);
        public IQueryable<T> GetWorkoutExercisesByWorkoutId(int workoutId);
        public int UpdateWorkoutExercise(T workoutExercise);
    }
}
