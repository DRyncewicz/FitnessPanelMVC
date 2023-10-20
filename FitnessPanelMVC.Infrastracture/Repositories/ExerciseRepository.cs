using FitnessPanelMVC.Domain.Interface;
using FitnessPanelMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.Repositories
{
    public class ExerciseRepository : IExerciseRepository<Exercise>
    {
        private readonly Context _dbCotenxt;
        public ExerciseRepository(Context dbContext)
        {
            _dbCotenxt = dbContext;
        }

        public int CreateExercise(Exercise exercise)
        {
            _dbCotenxt.Exercises.Add(exercise);
            _dbCotenxt.SaveChanges();
            return exercise.Id;
        }

        public void DeleteExercise(int id) 
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

        public IQueryable<Exercise> GetExercises()
        {
            var exercises = _dbCotenxt.Exercises;
            return exercises;
        }

        public IQueryable<Exercise> GetExercisesByNameContains(string name)
        {
            var exercises = _dbCotenxt.Exercises.Where(x => x.Name.Contains(name));
            return exercises;
        }


    }
}
