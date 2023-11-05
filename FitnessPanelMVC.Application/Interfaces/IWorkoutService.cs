using FitnessPanelMVC.Application.ViewModels.Workout;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IWorkoutService
    {
        Task<int> AddExerciseToWorkoutAsync(int exerciseId, int workoutId, int durationSeconds, double burnedCalories);

        Task<int> AddNewAsync(NewWorkoutVm newWorkoutVm, string userId);

        Task DeleteExerciseFromWorkoutByIdsAsync(int workoutId, int exerciseId);

        Task DeleteByIdAsync(int workoutId);

        Task<List<WorkoutForListVm>> GetAllForListAsync(DateTime Date, string userId);

        Task<WorkoutForListVm> GetDetailsByIdAsync(int workoutId);
    }
}
