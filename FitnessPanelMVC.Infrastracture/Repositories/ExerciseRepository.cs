using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
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

        public int Create(Exercise exercise)
        {
            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();
            return exercise.Id;
        }

        public void Delete(int id) 
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise != null)
            {
                _dbContext.Exercises.Remove(exercise);
                _dbContext.SaveChanges();
            }
        }

        public int UpdateExercise(Exercise exercise)
        {
            if (_dbContext.Exercises.Find(exercise.Id) != null)
            {
                _dbContext.Exercises.Update(exercise);
                _dbContext.SaveChanges();
                return exercise.Id;
            }
            return 0;
        }

        public IQueryable<Exercise> GetAll()
        {
            var exercises = _dbContext.Exercises;
            return exercises;
        }
    }
}
