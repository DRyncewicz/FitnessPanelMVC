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
        public void CreateWorkoutExercise(WorkoutExercise workoutExercise);
        public void DeleteWorkoutExercise(int workoutId, int exerciseId);
        public IQueryable<WorkoutExercise> GetAllWorkoutExercises();
        public void UpdateWorkoutExercise(WorkoutExercise workoutExercise);
    }
}
