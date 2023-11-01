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
        private readonly DbContext _dbCotenxt;

        public ExerciseRepository(DbContext dbContext)
        {
            _dbCotenxt = dbContext;
        }

        public int Create(Exercise exercise)
        {
            _dbCotenxt.Exercises.Add(exercise);
            _dbCotenxt.SaveChanges();
            return exercise.Id;
        }

        public void Delete(int id) 
        {
            var exercise = _dbCotenxt.Exercises.Find(id);
            if (exercise != null)
            {
                _dbCotenxt.Exercises.Remove(exercise);
                _dbCotenxt.SaveChanges();
            }
        }

        public int UpdateExercise(Exercise exercise)
        {
            if (_dbCotenxt.Exercises.Find(exercise.Id) != null)
            {
                _dbCotenxt.Exercises.Update(exercise);
                _dbCotenxt.SaveChanges();
                return exercise.Id;
            }
            return 0;
        }

        public IQueryable<Exercise> GetAll()
        {
            var exercises = _dbCotenxt.Exercises;
            return exercises;
        }
    }
}
