using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IWorkoutRepository
    {
        public int CreateWorkout(Workout workout);
        public void DeleteWorkout(int id);
        public IQueryable<Workout> GetAllWorkouts();
        public int UpdateWorkout(Workout workout);
    }
}
