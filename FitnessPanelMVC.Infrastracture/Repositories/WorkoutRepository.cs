using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly DbContext _dbContext;

        public WorkoutRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateWorkout(Workout workout)
        {
            _dbContext.Workouts.Add(workout);
            _dbContext.SaveChanges();
            return workout.Id;
        }

        public void DeleteWorkout(int id)
        {
            var workout = _dbContext.Workouts.Find(id);
            if (workout != null)
            {
                _dbContext.Workouts.Remove(workout);
                _dbContext.SaveChanges();
            }
        }

        public int UpdateWorkout(Workout workout)
        {
            if (_dbContext.Workouts.Find(workout.Id) != null)
            {
                _dbContext.Workouts.Update(workout);
                _dbContext.SaveChanges();
                return workout.Id;
            }
            return 0;
        }

        public IQueryable<Workout> GetAllWorkouts()
        {
            var workouts = _dbContext.Workouts;
            return workouts.AsQueryable();
        }
    }
}
