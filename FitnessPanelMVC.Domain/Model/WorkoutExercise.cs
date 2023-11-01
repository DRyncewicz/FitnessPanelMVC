using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class WorkoutExercise
    {
        public int WorkoutId { get; set; }

        public Workout Workout { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public TimeSpan Duration { get; set; }

        public double CaloriesBurned { get; set; }
    }
}
