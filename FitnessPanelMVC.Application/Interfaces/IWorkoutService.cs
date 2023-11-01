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

        int AddNew(NewWorkoutVm newWorkoutVm, string userId);

        void DeleteExerciseFromWorkoutByIds(int workoutId, int exerciseId);

        void DeleteById(int workoutId);

        List<WorkoutForListVm> GetAllForList(DateTime Date, string userId);

        WorkoutForListVm GetDetailsById(int workoutId);
    }
}
