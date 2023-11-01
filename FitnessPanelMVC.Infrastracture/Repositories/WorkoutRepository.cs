using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
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

        public int Create(Workout workout)
        {
            _dbContext.Workouts.Add(workout);
            _dbContext.SaveChanges();
            return workout.Id;
        }

        public void Delete(int id)
        {
            var workout = _dbContext.Workouts.Find(id);
            if (workout != null)
            {
                _dbContext.Workouts.Remove(workout);
                _dbContext.SaveChanges();
            }
        }

        public int Update(Workout workout)
        {
            if (_dbContext.Workouts.Find(workout.Id) != null)
            {
                _dbContext.Workouts.Update(workout);
                _dbContext.SaveChanges();
                return workout.Id;
            }
            return 0;
        }

        public IQueryable<Workout> GetAll()
        {
            var workouts = _dbContext.Workouts
                .Include(w => w.WorkoutExercises)
                .ThenInclude(w => w.Exercise);
            return workouts.AsQueryable();
        }
    }
}
