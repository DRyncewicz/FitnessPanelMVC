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
        int AddExerciseToWorkout(int exerciseId, int workoutId, int durationSeconds, double burnedCalories);
        int AddNewWorkout(NewWorkoutVm newWorkoutVm, string userId);
        void DeleteWorkoutById(int workoutId);
        List<WorkoutForListVm> GetAllUserWorkoutsForList(DateTime Date, string userId);
    }
}
