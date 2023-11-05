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
    public class WorkoutExerciseRepository : IWorkoutExerciseRepository
    {
        private readonly DbContext _dbContext;

        public WorkoutExerciseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(WorkoutExercise workoutExercise)
        {
            await _dbContext.WorkoutExercise.AddAsync(workoutExercise);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int workoutId, int exerciseId)
        {
            var workoutExercise = await _dbContext.WorkoutExercise
                .FirstOrDefaultAsync(we => we.WorkoutId == workoutId && we.ExerciseId == exerciseId);
            if (workoutExercise != null)
            {
                _dbContext.WorkoutExercise.Remove(workoutExercise);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(WorkoutExercise workoutExercise)
        {
            if (await _dbContext.WorkoutExercise.FindAsync(workoutExercise.WorkoutId, workoutExercise.ExerciseId) != null)
            {
                _dbContext.WorkoutExercise.Update(workoutExercise);
                await _dbContext.SaveChangesAsync();
            }
        }

        public IQueryable<WorkoutExercise> GetAll()
        {
            var workoutExercises = _dbContext.WorkoutExercise;
            return workoutExercises.AsQueryable();
        }
    }
}
