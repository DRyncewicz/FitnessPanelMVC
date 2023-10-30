using FitnessPanelMVC.Application.ViewModels.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IWorkoutService
    {
        List<WorkoutForListVm> GetAllUserWorkoutsForList(DateTime Date, string userId);
    }
}
