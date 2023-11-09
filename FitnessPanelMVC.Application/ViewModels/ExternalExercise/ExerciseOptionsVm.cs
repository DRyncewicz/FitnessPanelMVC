using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.ExternalExercise
{
    public class ExerciseOptionsVm
    {
        public List<string> AvailableOptions { get; set; }
        public string SelectedOption { get; set; }
        public List<ExternalExerciseVm> ExerciseResults { get; set; }
    }
}
