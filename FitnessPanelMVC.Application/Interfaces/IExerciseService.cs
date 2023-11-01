using FitnessPanelMVC.Application.ViewModels.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IExerciseService
    {
        int AddNew(ExerciseForListAndNewVm exerciseVm, string userId);

        List<ExerciseForListAndNewVm> GetForList();
    }
}
