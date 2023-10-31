using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class Workout
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double TotalCaloriesBurned { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Date { get; set; }
        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
