using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Application.ViewModels.WorkoutExercise.TransferModel
{
    public class WorkoutExerciseModel
    {
        public int ExerciseId { get; set; }
        public int DurationSeconds { get; set; }
        public double BurnedCalories { get; set; }
    }
}
