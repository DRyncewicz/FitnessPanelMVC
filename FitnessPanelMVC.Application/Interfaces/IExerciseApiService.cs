using FitnessPanelMVC.Application.ViewModels.ExternalExercise;
using FitnessPanelMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.Interfaces
{
    public interface IExerciseApiService
    {
        Task<List<ExternalExerciseVm>> GetExerciseVmToList(string bodyPartName);
    }
}
