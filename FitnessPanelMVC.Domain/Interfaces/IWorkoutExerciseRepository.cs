using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IWorkoutExerciseRepository
    {
        public void Create(WorkoutExercise workoutExercise);

        public void Delete(int workoutId, int exerciseId);

        public IQueryable<WorkoutExercise> GetAll();

        public void Update(WorkoutExercise workoutExercise);
    }
}
