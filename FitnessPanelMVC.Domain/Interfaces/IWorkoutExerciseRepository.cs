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
        public int CreateWorkoutExercise(WorkoutExercise workoutExercise);
        public void DeleteWorkoutExercise(int id);
        public IQueryable<WorkoutExercise> GetWorkoutExercisesByWorkoutId(int workoutId);
        public int UpdateWorkoutExercise(WorkoutExercise workoutExercise);
    }
}
