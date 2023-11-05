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
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly DbContext _dbContext;

        public ExerciseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAsync(Exercise exercise)
        {
            await _dbContext.Exercises.AddAsync(exercise);
            await _dbContext.SaveChangesAsync();
            return exercise.Id;
        }

        public async Task Delete(int id)
        {
            var exercise = await _dbContext.Exercises.FindAsync(id);
            if (exercise != null)
            {
                _dbContext.Exercises.Remove(exercise);
                await _dbContext.SaveChangesAsync();
            }
        }

        public IQueryable<Exercise> GetAll()
        {
            var exercises = _dbContext.Exercises;
            return exercises;
        }

        public async Task<Exercise> GetByIdAsync(int id)
        {
            var exercise = await _dbContext.Exercises.FirstOrDefaultAsync(r => r.Id == id);
            if (exercise == null)
            {
                exercise = new Exercise();
            }
            return exercise;
        }
    }
}
