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
        Task CreateAsync(WorkoutExercise workoutExercise);

        Task DeleteAsync(int workoutId, int exerciseId);

        IQueryable<WorkoutExercise> GetAll();

        Task UpdateAsync(WorkoutExercise workoutExercise);
    }
}
