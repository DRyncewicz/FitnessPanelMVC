using FitnessPanelMVC.Domain.Model.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Model
{
    public class Workout : BaseEntity
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string? Description { get; set; }

        public double TotalCaloriesBurned { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime Date { get; set; }

        public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
