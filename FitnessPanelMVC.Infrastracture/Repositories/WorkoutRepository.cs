using DocumentFormat.OpenXml.Packaging;
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

        public async Task<int> CreateAsync(Workout workout)
        {
            await _dbContext.Workouts.AddAsync(workout);
            await _dbContext.SaveChangesAsync();
            return workout.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var workout = await _dbContext.Workouts.FindAsync(id);
            if (workout != null)
            {
                _dbContext.Workouts.Remove(workout);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateAsync(Workout workout)
        {
            if (await _dbContext.Workouts.FindAsync(workout.Id) != null)
            {
                _dbContext.Workouts.Update(workout);
                await _dbContext.SaveChangesAsync();
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

        public async Task<Workout> GetByIdAsync(int id)
        {
            var workout = await _dbContext.Workouts.FirstOrDefaultAsync(w => w.Id == id);
            if (workout == null)
            {
                workout = new Workout();
            }
            return workout;
        }
    }
}
