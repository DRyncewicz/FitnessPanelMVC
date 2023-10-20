using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Domain.Interface
{
    public interface IWorkoutRepository<T>
    {
        public int CreateWorkout(T workout);
        public void DeleteWorkout(int id);
        public IQueryable<T> GetAllWorkouts();
        public int UpdateWorkout(T workout);
    }
}
