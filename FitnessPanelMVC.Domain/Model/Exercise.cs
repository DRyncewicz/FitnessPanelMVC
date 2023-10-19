using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double AverageCaloriesBurnedPerMinute { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
